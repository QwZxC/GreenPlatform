using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class GreenPlatformUser : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? AboutMe { get; set; }
    public string? AvatarPath { get; set; }
    public DateTime RegistrationDate { get; set; }
    public List<Role> Roles { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public List<Article> Articles { get; set; }
    [NotMapped]
    public List<Subscription> Subscriptions { get; set; }
    [NotMapped]
    public List<Subscription> Subscribers { get; set; }
}