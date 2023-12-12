using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutboxPattern.Application.Interfaces.Repositories;
using OutboxPattern.Application.Interfaces.Repositories.Base;
using OutboxPattern.Infrastructure.Context;
using OutboxPattern.Infrastructure.Repositories;
using OutboxPattern.Infrastructure.Repositories.Base;

namespace OutboxPattern.Infrastructure;
public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();

        services.AddEntityFrameworkNpgsql()
            .AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));

        return services;
    }
}
