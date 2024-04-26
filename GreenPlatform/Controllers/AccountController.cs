using Domain.Entities;
using GreenPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace GreenPlatform.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IRoleService _roleService;
    private readonly IHttpContextAccessor _contextAccessor;

    public AccountController(
        IUserService userService, 
        ITokenService tokenService,
        IRoleService roleService,
        IHttpContextAccessor contextAccessor)
    {
        _tokenService = tokenService;
        _userService = userService;
        _roleService = roleService;
        _contextAccessor = contextAccessor;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        GreenPlatformUser user = await _userService
            .FindUserByLoginAndPasswordAsync(model.Login, model.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }

        List<Role> userRoles = await _roleService.FindUserRoles(user);
        user.AccessToken = _tokenService.GenerateAccessToken(user, userRoles);
        await _userService.SaveAsync();

        _contextAccessor?.HttpContext.Response.Cookies.Append("Authorization", user.AccessToken);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        GreenPlatformUser user = await
            _userService.FindUserByLoginAsync(model.Login);

        if (user != null)
        {
            ModelState.AddModelError("", "Пользователь с таким логином уже зарегистрирован");
            return View(model);
        }

        user = await _userService.CreateUserAsync(model.Login, model.Password);
        user.AccessToken = _tokenService.GenerateAccessToken(user, user.Roles);
        user.RefreshToken = _tokenService.GenerateRefreshToken();
        await _userService.SaveAsync();

        _contextAccessor?.HttpContext.Response.Cookies.Append("Authorization", user.AccessToken);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        return View();
    }
}
