using ERP.Domain.Abstractions.Entities;

namespace ERP.Domain.Entities.Users;

public class RolePermission : EntityAuditBase
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }
}
