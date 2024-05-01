using Domain.Entities.Base;

namespace Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> FindByIdAsync(Guid id);
    void AddEntity(TEntity entity);
    void Update(TEntity entity);
    Task SaveAsync();
    void Delete(TEntity entity);
    Task<List<TEntity>> FindAllAsync();
}
