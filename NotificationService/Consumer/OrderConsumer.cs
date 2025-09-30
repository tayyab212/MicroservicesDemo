using MassTransit;
using Shared.Contracts;


namespace NotificationService.Consumers;

public class OrderConsumer : IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context)
    {
        var message = context.Message;
        Console.WriteLine($"[NotificationService] Order received: Id={message.OrderId}, Product={message.ProductName}");
        return Task.CompletedTask;
    }
}
