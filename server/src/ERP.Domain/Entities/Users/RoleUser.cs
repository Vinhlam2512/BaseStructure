using ERP.Domain.Abstractions.Entities;
using ERP.Domain.Entities.Identity;

namespace ERP.Domain.Entities.Users;

public class RoleUser : EntityAuditBase
{
    public RoleUser(Guid userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public int RoleId { get; set; }

    public Guid UserId { get; set; }


    public virtual User User { get; set; }

    public virtual Role Role { get; set; }
}
