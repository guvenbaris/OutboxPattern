using OutboxPattern.Infrastructure;
using OutboxPattern.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();
