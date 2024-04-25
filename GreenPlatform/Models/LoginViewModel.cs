using System.ComponentModel.DataAnnotations;

namespace GreenPlatform.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Это поле обязательно для ввода")]
    [Display(Name = "Логин")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для ввода")]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
}
