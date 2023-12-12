using Microsoft.EntityFrameworkCore;
using OutboxPattern.Application.Interfaces.Contexts;
using OutboxPattern.Domain.Entities;
using OutboxPattern.Shared.Model;

namespace OutboxPattern.Infrastructure.Context;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OrderEntity> Orders { get; set; }    
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OrderProductEntity> OrderProducts { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}
