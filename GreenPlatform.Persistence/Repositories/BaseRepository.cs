using Domain.Entities.Base;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public abstract class BaseRepository : IBaseRepository
{
    protected readonly GreenPlatformDbContext _context;

    public BaseRepository(GreenPlatformDbContext context)
    {
        _context = context;
    }

    public void AddEntity(BaseEntity entity)
    {
        _context.Add(entity);
    }

    public void Delete(BaseEntity entity)
    {
        _context.Remove(entity);
    }

    public async Task<List<BaseEntity>> FindAllAsync()
    {
        return await _context.Set<BaseEntity>().ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(BaseEntity entity)
    {
        _context.Update(entity);
    }
}
