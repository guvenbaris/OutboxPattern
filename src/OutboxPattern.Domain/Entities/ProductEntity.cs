using OutboxPattern.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutboxPattern.Domain.Entities;

[Table("product")]
public class ProductEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("stock_amount")]
    public int StockAmount { get; set; } = 1;

    [Column("description")]
    public string Description { get; set; } = string.Empty;
}
