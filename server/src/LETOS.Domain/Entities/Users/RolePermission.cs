using LETOS.Domain.Abstractions.Entities;

namespace LETOS.Domain.Entities.Users;

public class RolePermission : EntityAuditBase
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }
}
