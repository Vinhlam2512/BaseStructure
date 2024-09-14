using ERP.Domain.Abstractions.Entities;

namespace ERP.Domain.Entities.Identity;

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
