using OutboxPattern.Application.Interfaces.Repositories;
using OutboxPattern.Domain.Entities;
using OutboxPattern.Infrastructure.Context;
using OutboxPattern.Infrastructure.Repositories.Base;

namespace OutboxPattern.Infrastructure.Repositories;
public class OrderRepository(ApplicationDbContext context) : EfRepositoryBase<OrderEntity>(context), IOrderRepository
{
}
