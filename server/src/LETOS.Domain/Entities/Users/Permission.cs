﻿using LETOS.Domain.Abstractions.Entities;

namespace LETOS.Domain.Entities.Identity;

public sealed class Permission : EntityAuditBase<int>
{
    public static readonly Permission UsersRead = new(1, "Read");

    private Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; init; }
}
