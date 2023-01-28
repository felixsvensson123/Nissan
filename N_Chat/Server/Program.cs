global using Microsoft.EntityFrameworkCore;
global using N_Chat.Server.Data;
global using N_Chat.Shared;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR.Client;
using N_Chat.Client.Pages;
using N_Chat.Server.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
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
builder.Services.AddSignalR().AddHubOptions<SignalRController>
    (options =>
    {
        options.KeepAliveInterval = TimeSpan.FromMinutes(1);
        options.EnableDetailedErrors = true;
    });

var app = builder.Build();

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
app.MapFallbackToFile("index.html");
app.MapHub<SignalRController>("/conversations", options =>
{
    options.Transports =
        HttpTransportType.WebSockets |
        HttpTransportType.LongPolling;
});
var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:44392/conversations")
    .ConfigureLogging(logging => {
        logging.SetMinimumLevel(LogLevel.Information);
        logging.AddConsole();
    })
    .Build();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub("/conversations");
    endpoints.MapFallbackToPage("localhost:44392");
    endpoints.MapHub<SignalRController>(SignalRController.HubUrl);
});*/

app.Run();
