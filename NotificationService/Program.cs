using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Consumers;

var services = new ServiceCollection();

services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-queue", e =>
        {
            e.ConfigureConsumer<OrderConsumer>(context);
        });
    });
});

var provider = services.BuildServiceProvider();
var busControl = provider.GetRequiredService<IBusControl>();
await busControl.StartAsync();
try
{
    Console.WriteLine("NotificationService listening for orders. Press Enter to exit.");
    Console.ReadLine();
}
finally
{
    await busControl.StopAsync();
}
