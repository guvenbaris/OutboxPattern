using Microsoft.Extensions.DependencyInjection;
using OutboxPattern.Application.Utilities.Behaviors;

namespace OutboxPattern.Application;
public static class DependencyContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyContainer).Assembly));

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyContainer).Assembly);

            configuration.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });


        return services;
    }
}
