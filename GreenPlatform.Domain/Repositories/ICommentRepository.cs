using Domain.Entities;

namespace Domain.Repositories;

/// <summary>
/// Репозиторий для работы с сущностю comment
/// </summary>
public interface ICommentRepository : IBaseRepository<Comment>
{
    /// <summary>
    /// Осуществляет поиск комментариев к статье по её id
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    Task<List<Comment>> FindCommentsByArticleId(Guid articleId);
    /// <summary>
    /// Осуществляет поиск последнего комментария,
    /// который оставил пользователь,
    /// по id пользователя и id статьи
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Comment> FindLastUserCommentForArticleAsync(Guid articleId, Guid userId);
}