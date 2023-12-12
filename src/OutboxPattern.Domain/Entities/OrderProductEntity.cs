using OutboxPattern.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutboxPattern.Domain.Entities;

[Table("order_product")]
public class OrderProductEntity : BaseEntity
{
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [ForeignKey("ProductId")]
    [NotMapped]
    public ProductEntity? Product { get; set; } = null;

    [ForeignKey("OrderId")]
    [NotMapped]
    public OrderEntity? Order { get; set; } = null;
}
