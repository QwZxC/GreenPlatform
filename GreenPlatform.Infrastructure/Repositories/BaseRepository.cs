using Common.Exceptions;
using Domain.Entities.Base;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly GreenPlatformDbContext _context;

    protected BaseRepository(GreenPlatformDbContext context)
    {
        _context = context;
    }

    public void AddEntity(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public virtual async Task<List<TEntity>> FindAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> FindByIdAsync(Guid id)
    {
        return await _context
            .Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id) 
            ?? throw new NotFoundException($"Не удалось найти {typeof(TEntity)} c id: {id}");
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }
}
