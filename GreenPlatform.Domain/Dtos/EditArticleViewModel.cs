using Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class EditArticleViewModel
{
    public Guid ArticleId { get; set; }
    [Required(ErrorMessage = "Название обязательно для заполнения")]
    [DisplayName("Название")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Содержание обязательно для заполнения")]
    [DisplayName("Содержание")]
    public string Content { get; set; }
    [Required(ErrorMessage = "Добавьте тег в вашу статью")]
    [MinLength(1, ErrorMessage = "Выберите 1 тег для статьи")]
    [MaxLength(3, ErrorMessage = "Вы можете выбрать только 3 тега для статьи")]
    [DisplayName("Добавьте теги к своей статье")]
    public List<Guid> TagGuids { get; set; }

    public EditArticleViewModel(Article article) 
    { 
        ArticleId = article.Id;
        Title = article.Title;
        Content = article.Content;
    }
    public EditArticleViewModel() { }
}
