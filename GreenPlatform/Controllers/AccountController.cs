﻿using Domain.Entities;
using GreenPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace GreenPlatform.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
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
        
        GreenPlatformUser? user = await _userService
            .FindUserByLoginAndPasswordAsync(model.Login, model.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }

        await _userService.LoginAsync(user);
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

        if (await _userService.FindUserByLoginAsync(model.Login) != null)
        {
            ModelState.AddModelError("", "Пользователь с таким логином уже зарегистрирован");
            return View(model);
        }

        await _userService.LoginAsync(await _userService.CreateUserAsync(model.Login, model.Password));
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        return View();
    }
}