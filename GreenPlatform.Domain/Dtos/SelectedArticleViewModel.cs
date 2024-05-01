using Domain.Entities;

namespace Domain.Dtos;

public class SelectedArticleViewModel
{
    public Article SelectedArticle { get; set; }
    public List<Comment> Comments { get; set; }
}
