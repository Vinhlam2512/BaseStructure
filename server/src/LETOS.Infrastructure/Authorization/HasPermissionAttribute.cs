using LETOS.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace LETOS.Infrastructure.Authorization;
public sealed class HasPermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public string[] RequiredPermissions { get; }

    public HasPermissionAttribute(params string[] requiredPermissions)
    {
        RequiredPermissions = requiredPermissions;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;

        using var scope = httpContext.RequestServices.CreateScope();
        var authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

        string userName = httpContext.User.GetUserName();

        HashSet<string> permissions = await authorizationService.GetPermissionsForUserAsync(userName);

        if (permissions.Count == 0)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!RequiredPermissions.Any(requiredPerm => permissions.Contains(requiredPerm)))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}

