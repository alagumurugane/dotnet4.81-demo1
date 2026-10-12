using DemoWindowsService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "DemoWindowsService";
});
builder.Services.AddHostedService<HeartbeatService>();

await builder.Build().RunAsync();
