﻿using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Domain.Repositories;

namespace Core;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserService _userService; 

    public CommentService(ICommentRepository commentRepository, IUserService userService)
    {
        _commentRepository = commentRepository;
        _userService = userService;
    }

    public async Task CreateCommentAsync(CreateCommentViewModel viewModel)
    {
        Comment comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Content = viewModel.Content,
            ArticleId = viewModel.ArticleId,
            CreationDate = DateTime.UtcNow,
            CreatorId = _userService.GetAuthorizeUserId()
        };
        _commentRepository.AddEntity(comment);
        await _commentRepository.SaveAsync();
    }
}