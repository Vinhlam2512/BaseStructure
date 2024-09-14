using ERP.Domain.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ERP.Persistence.Interceptors;
public class SoftDeleteInterceptor : SaveChangesInterceptor
{
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

        IEnumerable<EntityEntry<ISoftDelete>> entries =
            dbContext
                .ChangeTracker
                .Entries<ISoftDelete>();

        foreach (EntityEntry<ISoftDelete> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.Property(a => a.DeletedAt).CurrentValue = DateTime.Now;
                entityEntry.Property(a => a.IsDelete).CurrentValue = true;

                entityEntry.State = EntityState.Modified;
            }
            else if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.DeletedAt).CurrentValue = null;
                entityEntry.Property(a => a.IsDelete).CurrentValue = false;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                // Optionally handle updates to ensure DeletedAt is not set accidentally
                if (entityEntry.Property(a => a.IsDelete).CurrentValue)
                {
                    entityEntry.Property(a => a.DeletedAt).CurrentValue ??= DateTime.Now;
                }
                else
                {
                    entityEntry.Property(a => a.DeletedAt).CurrentValue = null;
                }
            }

        }

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}
