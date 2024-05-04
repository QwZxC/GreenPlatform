﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Domain.Services;
using Common.Exceptions;
using Microsoft.Extensions.Logging;
using System;

namespace Core;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        IRoleService roleService,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IHttpContextAccessor contextAccessor,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _roleService = roleService;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    public async Task<GreenPlatformUser> CreateUserAsync(string login, string password)
    {
        Role role = await _roleService.FindRoleByNameAsync("User");
        string hashedPassword = _passwordHasher.Generate(password);
        GreenPlatformUser user = new()
        {
            Id = Guid.NewGuid(),
            Login = login,
            Password = hashedPassword,
            RegistrationDate = DateTime.UtcNow,
            Roles = new List<Role>() { role }
        };
        _userRepository.AddEntity(user);
        role.Users.Add(user);
        return user;
    }

    public async Task<List<GreenPlatformUser>> FindAllAsync()
    {
        return await _userRepository.FindAllAsync();
    }

    public async Task<GreenPlatformUser?> FindUserByLoginAndPasswordAsync(string login, string password)
    {
        GreenPlatformUser user = await _userRepository.FindByLoginAsync(login);

        if (_passwordHasher.Verify(password, user.Password))
        {
            return user;
        }

        return null;
    }

    public async Task<GreenPlatformUser> FindUserByLoginAsync(string login)
    {
        return await _userRepository.FindByLoginAsync(login);
    }

    public async Task LoginAsync(GreenPlatformUser user)
    {
        GenerateTokens(user);
        await SaveTokensAsync(user);
    }

    private void GenerateTokens(GreenPlatformUser user)
    {
        user.AccessToken = _tokenService.GenerateAccessToken(user, user.Roles);
        user.RefreshToken = _tokenService.GenerateRefreshToken();
    }
    private async Task SaveTokensAsync(GreenPlatformUser user)
    {
        await SaveAsync();
        _contextAccessor.HttpContext?.Response.Cookies.Append("Authorization", user.AccessToken);
    }

    public async Task SaveAsync()
    {
        await _userRepository.SaveAsync();
    }

    public Guid GetAuthorizeUserId()
    {
        var claim = _contextAccessor
            .HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == "UserId");
        Guid userId = Guid.Parse(claim?.Value ?? Guid.Empty.ToString());
        return userId;
    }

    public async Task LogOutAsync()
    {
        GreenPlatformUser user = await _userRepository
            .FindByIdAsync(GetAuthorizeUserId());
        user.AccessToken = string.Empty;
        _contextAccessor.HttpContext?.Response.Cookies.Delete("Authorization");
        await _userRepository.SaveAsync();
        _logger.LogInformation("{@User} вышел из аккаунта" +
            "\t\tВремя: {Time}\n" +
            "\t\tДата: {Date}\n", user, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString());
    }
}
