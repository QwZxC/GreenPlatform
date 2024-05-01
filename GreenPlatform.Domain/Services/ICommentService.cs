﻿using Domain.Dtos;
using Domain.Entities;

namespace Domain.Services;

public interface ICommentService
{
    Task CreateCommentAsync(CreateCommentViewModel viewModel);
    Task<List<Comment>> FindCommentsByArticleIdAsync(Guid articleId);
}