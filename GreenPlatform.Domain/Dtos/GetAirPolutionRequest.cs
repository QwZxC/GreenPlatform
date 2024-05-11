using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public record GetAirPolutionRequest
{
    [DisplayName("Город")]
    [Required(ErrorMessage = "это поле обязательно для заполнения")]
    public string City { get; set; }
}
