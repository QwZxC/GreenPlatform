using Domain.Entities;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using GreenPlatform.Domain.Dtos;

namespace GreenPlatform.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserService _userService;

    public AccountController(IUserService userService, ILogger<AccountController> logger)
    {
        _logger = logger;
        _userService = userService;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        GreenPlatformUser? user = await _userService
            .FindUserByLoginAndPasswordAsync(model.Login, model.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Неверный логин или пароль");
            Log("Неудачная попытка входа в аккаунт", model.Login);
            return View(model);
        }

        await _userService.LoginAsync(user);
        Log("Вход в аккаунт", model.Login);
        return RedirectToAction("Articles", "Article");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (await _userService.FindUserByLoginAsync(model.Login) != null)
        {
            ModelState.AddModelError("", "Пользователь с таким логином уже зарегистрирован");
            return View(model);
        }
        Log("Регистрация", model.Login);
        await _userService.LoginAsync(await _userService.CreateUserAsync(model.Login, model.Password));
        return RedirectToAction("Articles", "Article");
    }

    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        Log("Выход из аккаунта");
        await _userService.LogOutAsync();
        return RedirectToAction("Login");
    }

    public async Task<IActionResult> PersonalAccount(string login)
    {
        ViewBag.User = await _userService.FindUserByLoginAsync(login);
        return View();
    }

    [Authorize]
    public async Task<IActionResult> EditAccountInfo()
    {
        ViewBag.User = await _userService.FindByIdAsync(_userService.GetAuthorizeUserId());
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EditAccountInfo(EditAccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _userService.EditAccountInfoAsync(model);
        UserDto user = await _userService.FindByIdAsync(_userService.GetAuthorizeUserId());
        return Redirect($"PersonalAccount?login={user.Login}");
    }

    public async Task<IActionResult> GetUserAvatarName(Guid userId)
    {
        string avatarPath = await _userService.GetUserAvatarNameAsync(userId);
        return !string.IsNullOrWhiteSpace(avatarPath) ? Ok(avatarPath) : Ok("empty-avatar.jpg");
    }


    [NonAction]
    private void Log(string action, string login = "")
    {
        _logger.LogInformation("{Action} {Login}\n" + 
            "\t\tВремя: {Time}\n" +
            "\t\tДата: {Date}\n",
            action, login, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString());
    }

    [Authorize]
    public async Task<ActionResult<Guid>> GetAuthorizedUserId()
    {
        return Ok(_userService.GetAuthorizeUserId());
    }
}