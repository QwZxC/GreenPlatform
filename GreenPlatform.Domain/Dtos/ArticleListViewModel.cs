using System.ComponentModel;

namespace Domain.Dtos;

public class ArticleListViewModel
{
    public string Title { get; set; } = string.Empty;
    [DisplayName("Отсортировать по")]
    public string PropertyNameToSorting { get; set; } = "CreationDate";
}
