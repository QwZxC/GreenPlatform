using Domain.Entities.Base;

namespace Domain.Repositories;

public interface IBaseRepository
{
    void AddEntity(BaseEntity entity);
    void Update(BaseEntity entity);
    Task SaveAsync();
    void Delete(BaseEntity entity);
    Task<List<BaseEntity>> FindAllAsync();
}
