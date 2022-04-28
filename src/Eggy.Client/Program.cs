using Eggy.Client;
using Eggy.Client.Brokers;
using Eggy.Domain.Brokers;
using Eggy.Domain.Processing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLocalStorageServices();
builder.Services.AddScoped<IStorageBroker, StorageBroker>();
builder.Services.AddScoped<IProjectProcessor, ProjectProcessor>();
builder.Services.AddScoped<IWeekTimeProcessor, WeekTimeProcessor>();

await builder.Build().RunAsync();
