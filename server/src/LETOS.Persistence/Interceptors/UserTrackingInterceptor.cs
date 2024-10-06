using System.Security.Claims;
using LETOS.Domain.Abstractions.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LETOS.Persistence.Interceptors;
public class UserTrackingInterceptor : SaveChangesInterceptor
{


    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserTrackingInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        IEnumerable<EntityEntry<IUserTracking>> entries =
            dbContext
                .ChangeTracker
                .Entries<IUserTracking>();


        if (entries.Count() > 0)
        {
            string? id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Sid)?.Value;
            if (id != null)
            {
                var currentUserId = new Guid(id);

                foreach (EntityEntry<IUserTracking> entityEntry in entries)
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property(a => a.CreatedBy).CurrentValue = currentUserId;
                    }
                    else if (entityEntry.State == EntityState.Deleted || entityEntry.State == EntityState.Modified)
                    {
                        entityEntry.Property(a => a.ModifiedBy).CurrentValue = currentUserId;
                    }
                }
            }
        }


        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}
