using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class CreateArticleViewModel
{
    [Required(ErrorMessage = "У статьи должно быть название")]
    [DisplayName("Название")]
    [MaxLength(255, ErrorMessage = $"Название статьи не должно превышать 255 символов")]
    public string Title { get; set; }
    [Required(ErrorMessage = "У статьи должно быть содержание")]
    [DisplayName("Содержание")]
    [MinLength(100, ErrorMessage = "Слишком маленькая статья.")]
    public string Content { get; set; }
    public string PreviousUrl { get; set; }
    [Required(ErrorMessage = "Добавьте тег в вашу статью")]
    [MinLength(1, ErrorMessage = "Выберите 1 тег для статьи")]
    [MaxLength(3, ErrorMessage = "Вы можете выбрать только 3 тега для статьи")]
    public List<Guid> TagGuids { get; set; }
}