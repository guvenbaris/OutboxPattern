namespace OutboxPattern.Shared.Events.Orders;

public record class OrderQuantityControlEvent(Guid OrderId, List<ProductQuantity> ProductQuantities);

public record class ProductQuantity(Guid ProductId, int Quantity);