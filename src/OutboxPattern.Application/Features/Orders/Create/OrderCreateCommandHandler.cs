using MediatR;
using OutboxPattern.Application.Interfaces.Repositories;
using OutboxPattern.Application.Utilities.Results;
using OutboxPattern.Domain.Entities;
using OutboxPattern.Shared.Constants;
using OutboxPattern.Shared.OutboxEvent;

namespace OutboxPattern.Application.Features.Orders.Create;

public sealed class OrderCreateCommandHandler(IOrderRepository orderRepository) : IRequestHandler<OrderCreateCommand, Result>
{
    private readonly Guid customerId = Guid.NewGuid();
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Result> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        if (request.ProductQuantities == null)
        {
            return await Result.Problem(errorMessage: "Please select products");
        }

        var order = new OrderEntity
        {
            Description = request.Description,
            CustomerId = customerId,
        };

        List<OrderProductEntity> orderProductEntities = new();
        foreach (var productQuantity in request.ProductQuantities)
        {
            var orderProductEntity = new OrderProductEntity
            {
                ProductId = productQuantity.ProductId,
                OrderId = order.Id,
                Quantity = productQuantity.Quantity,                    
            };

            orderProductEntities.Add(orderProductEntity);
        }

        OutboxEventHandler.CreateOutboxEvent(EventConstant.OrderQuantityControlEvent, entities: orderProductEntities);        

        await _orderRepository.AddAsync(order);

        return await Result.Success();
    }
}
