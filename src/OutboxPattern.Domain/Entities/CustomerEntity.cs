using OutboxPattern.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutboxPattern.Domain.Entities;

[Table("customer")]
public class CustomerEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("surname")]
    public string Surname { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("balance")]
    public decimal Balance { get; set; } = 0;
}
