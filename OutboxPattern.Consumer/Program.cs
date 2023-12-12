using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OutboxPattern.Consumer;

var builder = Host.CreateDefaultBuilder(args);

var host = builder.ConfigureServices((hostContext, services) =>
{
    var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    services.AddConsumerServices(builder.Build());
})
.Build();
await host.RunAsync();