using MassTransit;
using MediatR;
using Shared.Contracts;

namespace OrderService.Commands
{
 
    // Handler now implements IRequestHandler
    public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, string>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PlaceOrderHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<string> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            // Create shared message
            var orderMessage = new Order
            {
                OrderId = request.OrderId,
                ProductName = request.ProductName
            };

            // Publish to RabbitMQ
            await _publishEndpoint.Publish(orderMessage, cancellationToken);

            return $"Order {request.OrderId} placed for {request.ProductName}";
        }
    }
}
