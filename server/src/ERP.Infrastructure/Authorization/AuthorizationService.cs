using ERP.Application.Abstractions.Caching;
using ERP.Domain.Entities.Identity;
using ERP.Domain.Entities.Users;
using ERP.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Authorization;
internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string userName)
    {
        string cacheKey = $"auth:roles-{userName}";
        UserRolesResponse? cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cachedRoles is not null)
        {
            return cachedRoles;
        }

        UserRolesResponse roles = await _dbContext.Set<User>()
            .Where(u => u.Name.UserName == userName)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.RoleUsers.Select(ru => ru.Role).ToList()
            })
            .FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles);

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string userName)
    {
        string cacheKey = $"auth:permissions-{userName}";
        HashSet<string>? cachedPermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (cachedPermissions is not null)
        {
            return cachedPermissions;
        }

        ICollection<Permission> permissions = await _dbContext.Set<User>()
            .Where(u => u.Name.UserName == userName)
            .SelectMany(u => u.RoleUsers.Select(ru => ru.Role).Select(r => r.Permissions))
            .FirstAsync();

        var permissionsSet = permissions.Select(p => p.Name).ToHashSet();

        await _cacheService.SetAsync(cacheKey, permissionsSet);

        return permissionsSet;
    }
}

