using ERP.Domain.Abstractions.Entities;
using ERP.Domain.Entities.Users;

namespace ERP.Domain.Entities.Identity;

public sealed class Role : EntityAuditBase<int>
{
    public static readonly Role User = new(1, "User");

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; init; }

    public ICollection<RoleUser> RoleUsers { get; init; } = new List<RoleUser>();

    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();

}
