﻿using Domain.Entities;

namespace Service.Abstractions;

public interface IRoleService
{
    Task<Role> FindRoleByNameAsync(string name);
}
