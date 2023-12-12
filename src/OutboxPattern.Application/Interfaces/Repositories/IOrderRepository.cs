using OutboxPattern.Application.Interfaces.Repositories.Base;
using OutboxPattern.Domain.Entities;

namespace OutboxPattern.Application.Interfaces.Repositories;
public interface IOrderRepository : IRepositoryBase<OrderEntity>
{
}
