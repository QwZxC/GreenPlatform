﻿namespace GreenPlatform.Domain.Dtos;

public record UserDto(Guid Id, string Login, string? AboutMe, string? AvatarPath);