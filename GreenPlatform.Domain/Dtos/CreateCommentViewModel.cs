using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class CreateCommentViewModel
{
    [DisplayName("Содержание")]
    [Required(ErrorMessage = "У комметария должно быть содержание")]
    public string Content { get; set; }
    public Guid ArticleId { get; set; }
}
