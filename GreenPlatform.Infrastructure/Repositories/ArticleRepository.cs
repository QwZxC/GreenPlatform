using Common.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ArticleRepository : BaseRepository<Article>, IArticleRepository
{
    public ArticleRepository(GreenPlatformDbContext context) : base(context)
    {
    }

    public async override Task<List<Article>> FindAllAsync()
    {
        return await _context.Article.Include(article => article.Owner).ToListAsync();
    }

    public async Task<Article> FindArticleByIdAsync(Guid id)
    {
        return await _context.Article
            .Include(article => article.Owner)
            .Include(article => article.Comments)
            .FirstOrDefaultAsync(article => article.Id == id)
            ?? throw new NotFoundException("Статья не найдена");
    }
}
