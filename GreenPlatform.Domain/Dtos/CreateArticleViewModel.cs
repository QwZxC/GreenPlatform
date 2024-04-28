using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class CreateArticleViewModel
{
    [Required(ErrorMessage = "У статьи должно быть название")]
    [DisplayName("Название")]
    public string Title { get; set; }
    [Required(ErrorMessage = "У статьи должно быть содержание")]
    [DisplayName("Содержание")]
    public string Content { get; set; }
}