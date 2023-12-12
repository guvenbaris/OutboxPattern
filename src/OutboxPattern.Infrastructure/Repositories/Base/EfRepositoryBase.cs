using Microsoft.EntityFrameworkCore;
using OutboxPattern.Application.Interfaces.Repositories.Base;
using OutboxPattern.Domain.Entities.Base;
using OutboxPattern.Infrastructure.Context;
using System.Linq.Expressions;

namespace OutboxPattern.Infrastructure.Repositories.Base;
public class EfRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private DbSet<TEntity> _dbSet { get => _context.Set<TEntity>(); }

    public EfRepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        // Expression'a ekleme yapılacak silinmemişler gelmesin
        return await _dbSet.Where(expression).ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
         await Task.FromResult(_dbSet.Update(entity));
    }
}
