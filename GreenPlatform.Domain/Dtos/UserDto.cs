using Domain.Entities;

namespace GreenPlatform.Domain.Dtos;

public record UserDto(
    Guid Id, 
    string Login,
    string? AboutMe,
    string? AvatarPath,
    List<Article> Articles,
    List<Subscription> Subscriptions,
    List<Subscription> Subscribers);