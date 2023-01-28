global using N_Chat.Shared.dto;
global using N_Chat.Shared;
global using MudBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using N_Chat.Client;
using N_Chat.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITestService, TestService>(); // Initiazilerar test service i client projektet s� det kan n�s i codebehinden p� en sida // bara ett exempel //
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddMudServices();
builder.Services.AddSingleton<HubConnection>(sp => {
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
        .WithUrl(navigationManager.ToAbsoluteUri("/conversations"))
        .WithAutomaticReconnect()
        .Build();
});
await builder.Build().RunAsync();
