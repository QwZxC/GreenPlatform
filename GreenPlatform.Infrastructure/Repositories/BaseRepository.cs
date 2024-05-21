using Common.Exceptions;
using Domain.Entities.Base;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация базового репозитория
/// </summary>
/// <typeparam name="TEntity"></typeparam>
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

    /// <summary>
    /// В случае не нахождения подходящего элемента выкидывает исключение
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public virtual async Task<TEntity> FindByIdAsync(Guid id)
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

    public Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderByExpression<T>(string propertyName)
    {
        Type typeQueryable = typeof(IQueryable<T>);
        ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
        var outerExpression = Expression.Lambda(argQueryable, argQueryable);

        var entityType = typeof(T);
        ParameterExpression arg = Expression.Parameter(entityType, "x");

        Expression expression = arg;

        PropertyInfo propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        expression = Expression.Property(expression, propertyInfo);

        LambdaExpression lambda = Expression.Lambda(expression, arg);

        MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                                                         "OrderBy",
                                                         [typeof(T), lambda.ReturnType],
                                                         outerExpression.Body,
                                                         Expression.Quote(lambda));

        var finalLambda = Expression.Lambda(resultExp, argQueryable);

        return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
    }
}
