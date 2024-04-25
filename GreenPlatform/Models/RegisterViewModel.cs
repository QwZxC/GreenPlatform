using System.ComponentModel.DataAnnotations;

namespace GreenPlatform.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Это поле обязательно для ввода")]
    [Display(Name = "Логин")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для ввода")]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Это поле обязательно для ввода")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
    [Display(Name = "Повторить пароль")]
    public string RepeatPassword { get; set; }
}
