using ERP.Domain.Entities.Identity;

namespace ERP.Infrastructure.Authorization;
internal sealed class UserRolesResponse
{
    public Guid UserId { get; init; }

    public List<Role> Roles { get; init; } = [];
}
