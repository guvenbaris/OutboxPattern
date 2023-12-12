using MassTransit;
using Microsoft.Extensions.Hosting;
using OutboxPattern.Pulling;
using Quartz;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddQuartz(configurator =>
        {       
            JobKey jobKey = new("OutboxPublishJob");

            configurator.AddJob<OutboxPublishJob>(options => options.WithIdentity(jobKey));

            TriggerKey triggerKey = new("OutboxPublishTrigger");

            configurator.AddTrigger(options => options.ForJob(jobKey)
                        .WithIdentity(triggerKey)
                        .StartAt(DateTime.UtcNow)
                        .WithSimpleSchedule
                        (
                            builder => builder.WithIntervalInSeconds(5)
                                              .RepeatForever()
                        ));
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
                });

                cfg.SetQuorumQueue();
            });
        });
    })
    .Build();

await host.RunAsync();