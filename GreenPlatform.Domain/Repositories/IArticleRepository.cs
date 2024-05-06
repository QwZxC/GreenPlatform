﻿using Domain.Entities;
using Domain.Repositories;

namespace Domain.Repositories;

public interface IArticleRepository : IBaseRepository<Article>
{
    /// <summary>
    /// Осуществляет поиск статей, написанных пользователем
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<Article>> FindAllArticlesForUserAsync(Guid userId);
}
