using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderService.Commands;
using System.Reflection;

var services = new ServiceCollection();

// Logging
services.AddLogging(cfg => cfg.AddConsole());



// Register MediatR handlers
services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// MassTransit + RabbitMQ
services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

var busControl = provider.GetRequiredService<IBusControl>();
await busControl.StartAsync();
try
{
    // Place example orders
    await mediator.Send(new PlaceOrderCommand { OrderId = 1, ProductName = "Laptop" });
    await mediator.Send(new PlaceOrderCommand { OrderId = 2, ProductName = "Phone" });

    Console.WriteLine("Orders placed and published to RabbitMQ.");
    Console.WriteLine("Press Enter to exit.");
    Console.ReadLine();
}
finally
{
    await busControl.StopAsync();
}
