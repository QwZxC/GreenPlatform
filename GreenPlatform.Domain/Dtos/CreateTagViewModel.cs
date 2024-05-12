using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class CreateTagViewModel
{
    [Required(ErrorMessage ="У тега должно быть название")]
    [DisplayName("Название")]
    public string Name { get; set; }
}
