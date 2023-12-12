using OutboxPattern.Application.Interfaces.Repositories;
using OutboxPattern.Domain.Entities;
using OutboxPattern.Infrastructure.Context;
using OutboxPattern.Infrastructure.Repositories.Base;

namespace OutboxPattern.Infrastructure.Repositories;
public class OrderProductRepository(ApplicationDbContext context) : EfRepositoryBase<OrderProductEntity>(context), IOrderProductRepository
{
}