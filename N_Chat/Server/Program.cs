global using Microsoft.EntityFrameworkCore;
global using N_Chat.Server.Data;
global using N_Chat.Shared;
global using Azure.Identity;
global using Azure.Security.KeyVault.Keys;
global using N_Chat.Client.Services;
global using Azure.Security.KeyVault;
using Azure.Security.KeyVault.Keys.Cryptography;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Azure;
using N_Chat.Client.Pages;
using N_Chat.Client.Services;
using N_Chat.Server.Controllers;

var builder = WebApplication.CreateBuilder(args);


var credential = new DefaultAzureCredential();
KeyClient keyClient = new KeyClient(new Uri("https://enchatvault.vault.azure.net/"), credential);
KeyVaultKey vaultKey = keyClient.GetKey("MessageKey");
CryptographyClient cryptoClient = new CryptographyClient(vaultKey.Id, credential);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

// Add database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

//Add identityuser to db
builder.Services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
});

//Adds signalR service
builder.Services.AddSignalR()
    .AddHubOptions<SignalRController>
    (options =>
    {
        options.KeepAliveInterval = TimeSpan.FromSeconds(10);
        options.ClientTimeoutInterval = TimeSpan.FromDays(155);
        options.EnableDetailedErrors = true;
    });

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        });

/*builder.Services.AddResponseCompression(options => options.MimeTypes = 
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes
        .Concat(new[] {"application/octet-stream"}));*/

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsSpecs",
    builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(options => true)
            .AllowCredentials();
    });
});
//adds services for azure key vault 
builder.Services.AddSingleton(keyClient);
builder.Services.AddSingleton(vaultKey);
builder.Services.AddSingleton(cryptoClient);
builder.Services.AddSingleton<IKeyVaultService>(new KeyVaultService(keyClient, cryptoClient, vaultKey));
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

//app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapHub<SignalRController>("/conversations");
app.MapFallbackToFile("index.html");

app.UseCors("CorsSpecs");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
