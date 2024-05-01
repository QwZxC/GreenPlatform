using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class EditArticleViewModel
{
    public Guid ArticleId { get; set; }
    [Required(ErrorMessage = "Название обязательно для заполнения")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Содержание обязательно для заполнения")]
    public string Content { get; set; }

    public EditArticleViewModel(Article article) 
    { 
        ArticleId = article.Id;
        Title = article.Title;
        Content = article.Content;
    }
    public EditArticleViewModel() { }
}
