using Domain.Entities.Base;

namespace Domain.Repositories;

/// <summary>
/// Базовый репозиторий, содержащий в себе общую логику для всех сущностей
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Осуществляет поиск сущности по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> FindByIdAsync(Guid id);
    /// <summary>
    /// Добавляет сущность в колекцию DbContext
    /// </summary>
    /// <param name="entity"></param>
    void AddEntity(TEntity entity);
    /// <summary>
    /// Обновляет информацию о сущности в коллекции DbContext
    /// </summary>
    /// <param name="entity"></param>
    void Update(TEntity entity);
    /// <summary>
    /// Сохраняет ранее изменёные данные
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();
    /// <summary>
    /// Удаляет сущность из коллекции DbContext
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);
    /// <summary>
    /// Осуществляет поиск всех сущностей
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> FindAllAsync();
}
