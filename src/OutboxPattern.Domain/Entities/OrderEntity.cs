using OutboxPattern.Domain.Entities.Base;
using OutboxPattern.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutboxPattern.Domain.Entities;

[Table("order")]
public class OrderEntity : BaseEntity
{
    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("customer_id")]
    public Guid CustomerId { get; set; }

    [Column("status")]
    public int Status { get; set; } = (int)OrderStatus.Preparing;

    [ForeignKey("CustomerId")]
    [NotMapped]
    public CustomerEntity? Customer { get; } = null;
}
