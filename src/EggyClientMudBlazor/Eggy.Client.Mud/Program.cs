using Eggy.Client.Mud;
using Eggy.Client.Mud.Brokers;
using Eggy.Domain.Brokers;
using Eggy.Domain.Processing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLocalStorageServices();
builder.Services.AddScoped<IStorageBroker, StorageBroker>();
builder.Services.AddScoped<IProjectProcessor, ProjectProcessor>();
builder.Services.AddScoped<IWeekTimeProcessor, WeekTimeProcessor>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
