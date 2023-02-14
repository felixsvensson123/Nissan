global using N_Chat.Shared.dto;
global using N_Chat.Shared;
global using MudBlazor.Services;
global using Microsoft.AspNetCore.SignalR.Client;
global using  Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
global using N_Chat.Client;
global using N_Chat.Client.Handlers;
global using N_Chat.Client.Services;
global using Azure.Identity;
global using Azure.Security.KeyVault.Keys;
global using Azure.Security.KeyVault;
global using Azure.Security.KeyVault.Keys.Cryptography;
global using Azure.Security.KeyVault.Secrets;
global using Azure.Core;
using Azure.Core.Pipeline;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Azure;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<ITestService, TestService>(); // Initiazile test service i client projektet s� det kan n�s i codebehinden p� en sida // bara ett exempel //
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddMudServices();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthentication();
builder.Services.AddBlazoredLocalStorage();
builder.Services
    .AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CookieHandler>();
builder.Services.AddHttpClient("API", options => {
        options.BaseAddress = new Uri("https://localhost:7280/");
    })
    .AddHttpMessageHandler<CookieHandler>();
await builder.Build().RunAsync();
