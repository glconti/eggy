using Blazored.LocalStorage;
using Eggy.Client;
using Eggy.Client.Domain.Brokers;
using Eggy.Client.Domain.Processing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IStorageBroker, StorageBroker>();
builder.Services.AddScoped<IProjectProcessor, ProjectProcessor>();

await builder.Build().RunAsync();
