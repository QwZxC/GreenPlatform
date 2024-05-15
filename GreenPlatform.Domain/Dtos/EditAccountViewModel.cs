using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class EditAccountViewModel
{
    [DisplayName("Аватар профиля")]
    public IFormFile? ProfileImage { get; set; }
    
    [DisplayName("О себе")]
    public string AboutMe { get; set; }
    [Required(ErrorMessage = "Это поле обязательно для заполнения")]
    [DisplayName("Логин")]
    public string Login { get; set; }
}