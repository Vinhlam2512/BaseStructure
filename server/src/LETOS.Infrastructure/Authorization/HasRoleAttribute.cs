using LETOS.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace LETOS.Infrastructure.Authorization;
public sealed class HasRoleAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public string[] Roles { get; }

    public HasRoleAttribute(params string[] roles)
    {
        Roles = roles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;

        using var scope = httpContext.RequestServices.CreateScope();
        var authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

        string userName = httpContext.User.GetUserName();

        UserRolesResponse rolesResponse = await authorizationService.GetRolesForUserAsync(userName);

        if (rolesResponse != null && rolesResponse.Roles.Count == 0)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!rolesResponse.Roles.Any(role => Roles.Contains(role.Name)))
        {
            context.Result = new ForbidResult();
            return;
        }

    }


}
