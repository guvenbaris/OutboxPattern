using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutboxPattern.Consumer.Consumers;
using OutboxPattern.Infrastructure.Context;

namespace OutboxPattern.Consumer;
public static class ConsumerWorkerExtension
{
    public static IServiceCollection AddConsumerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderQuantityControlConsumer>();
            x.AddConsumer<CustomerMoneyGiftConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("stock-order-control-event", e =>
                {
                    e.ConfigureConsumer<OrderQuantityControlConsumer>(ctx);
                    e.SetQuorumQueue();
                });

                cfg.ReceiveEndpoint("customer-money-gift-event", e =>
                {
                    e.ConfigureConsumer<CustomerMoneyGiftConsumer>(ctx);
                    e.SetQuorumQueue();
                });
            });
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddEntityFrameworkNpgsql()
            .AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));

        return services;
    }
}
