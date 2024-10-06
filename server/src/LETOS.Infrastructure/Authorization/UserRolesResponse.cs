using LETOS.Domain.Entities.Identity;

namespace LETOS.Infrastructure.Authorization;
internal sealed class UserRolesResponse
{
    public Guid UserId { get; init; }

    public List<Role> Roles { get; init; } = [];
}
