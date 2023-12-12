using OutboxPattern.Domain.Entities.Base;
using System.Linq.Expressions;

namespace OutboxPattern.Application.Interfaces.Repositories.Base;
public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{   
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity,bool>> expression);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);    
}
