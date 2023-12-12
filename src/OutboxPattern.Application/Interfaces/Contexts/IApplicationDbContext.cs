using Microsoft.EntityFrameworkCore;
using OutboxPattern.Domain.Entities;

namespace OutboxPattern.Application.Interfaces.Contexts;
public interface IApplicationDbContext 
{
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OrderProductEntity> OrderProducts { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }

}
