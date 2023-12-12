using MassTransit;
using Microsoft.EntityFrameworkCore;
using OutboxPattern.Domain.Entities;
using OutboxPattern.Domain.Enums;
using OutboxPattern.Infrastructure.Context;
using OutboxPattern.Shared.Events.Orders;

namespace OutboxPattern.Consumer.Consumers;
public sealed class OrderQuantityControlConsumer(ApplicationDbContext context) : IConsumer<OrderQuantityControlEvent>
{
    private readonly ApplicationDbContext _context = context;

    public async Task Consume(ConsumeContext<OrderQuantityControlEvent> orderQuantity)
    {
        if (orderQuantity == null || orderQuantity.Message == null)
            return;
        
        OrderStatus orderStatus = OrderStatus.Preparing;

        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderQuantity.Message.OrderId);

        if (order == null)          
            return;

        var productAmounts = _context.Products
            .Where(x => orderQuantity.Message.ProductQuantities.Select(p => p.ProductId).Contains(x.Id));

        foreach (var productGroup in orderQuantity.Message.ProductQuantities.GroupBy(x => x.ProductId))
        {
            var requestedQuantityByProduct = productGroup.Sum(x => x.Quantity);
            var product = productAmounts.First(x => x.Id == productGroup.Key);
            var stockAmount = product.StockAmount;

            if (stockAmount < requestedQuantityByProduct)
            {
                orderStatus = OrderStatus.Declined; 
                break;
            }

            orderStatus = OrderStatus.Confirmed;

            productAmounts.First(x => x.Id == productGroup.Key).StockAmount = stockAmount - requestedQuantityByProduct;
        }

        order.Status = (int)orderStatus;

        if (orderStatus == OrderStatus.Declined)
        {
            order.Description = "Stock amount doesn't enough";
        }
        else
        {
            _context.Set<ProductEntity>().UpdateRange(productAmounts);
        }

        _context.Set<OrderEntity>().Update(order);

        await _context.SaveChangesAsync();

        return;
    }
}

