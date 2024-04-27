using Domain.Entities.Base;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly GreenPlatformDbContext _context;

    public BaseRepository(GreenPlatformDbContext context)
    {
        _context = context;
    }

    public void AddEntity(TEntity entity)
    {
        _context.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public virtual async Task<List<TEntity>> FindAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
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
