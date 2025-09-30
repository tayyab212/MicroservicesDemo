using MediatR;

namespace OrderService.Commands;

public class PlaceOrderCommand : IRequest<string>
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
}
