using Common.Exceptions;
using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация IArticleRepository.
/// Так же реализует в себе IBaseRepository,
/// путём наследования от BaseRepository
/// </summary>
public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(GreenPlatformDbContext context) : base(context)
    {
    }

    public async Task<List<Article>> FindAllArticelsByTitle(ArticleListViewModel vm)
    {
        IQueryable<Article> query = _context.Article.Include(article => article.Comments)
            .Include(article => article.Tags)
            .Include(article => article.Owner).ThenInclude(owner => owner.Subscribers)
            .Where(article => article.Title.Contains(vm.Title));
        var orderBy = GetOrderByExpression<Article>(vm.PropertyNameToSorting);
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return await query.ToListAsync();
    }

    public async Task<List<Article>> FindAllArticlesForUserAsync(Guid userId)
    {
        return await _context.Article.Where(article => article.OwnerId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Переопределённый метод.
    /// Осуществляет поиск всех статей,
    /// а так же включает в себя информацию о владельце статьи
    /// </summary>
    /// <returns></returns>
    public async override Task<List<Article>> FindAllAsync()
    {
        return await _context.Article.Include(article => article.Tags).Include(article => article.Owner).ToListAsync();
    }

    /// <summary>
    /// Переопределённый метод,
    /// Осуществляет поиск статьи по id,
    /// а так же включает в себя информацию о владельце статьи
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public override async Task<Article> FindByIdAsync(Guid id)
    {
        return await _context.Article
            .Include(article => article.Tags)
            .Include(article => article.Owner)
            .FirstOrDefaultAsync(article => article.Id == id)
            ?? throw new NotFoundException("Статья не найдена");
    }
}
