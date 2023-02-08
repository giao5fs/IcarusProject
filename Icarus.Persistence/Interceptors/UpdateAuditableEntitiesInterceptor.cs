using Icarus.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Icarus.Persistence.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor
    : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(a => a.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(a => a.LastModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
