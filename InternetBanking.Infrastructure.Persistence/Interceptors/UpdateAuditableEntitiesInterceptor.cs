using InternetBanking.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InternetBanking.Infrastructure.Persistence.Interceptors
{
    public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<IAuditableEntity>> entries = dbContext.ChangeTracker
                .Entries<IAuditableEntity>();

            foreach (EntityEntry<IAuditableEntity> entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
    }
}