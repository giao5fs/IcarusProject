using Icarus.Application.Abstractions.Data;
using Icarus.Domain.Abtractions;
using Icarus.Domain.Entities;
using Icarus.Domain.Primitives;
using Icarus.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Icarus.Persistence;

public sealed class IcarusDbContext : DbContext, IDbContext, IUnitOfWork
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Member> Members { get; set; }

    public IcarusDbContext(DbContextOptions<IcarusDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        => base.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync<TEntity>(Guid id) where TEntity : Entity
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Insert<TEntity>(TEntity entity) where TEntity : Entity
        => Set<TEntity>().Add(entity);

    void IDbContext.Remove<TEntity>(TEntity entity)
        => Set<TEntity>().Remove(entity);

    public Task SaveChangeAsync(CancellationToken cancellation = default)
    {
        UpdateAuditableEntities();

        UpdateSoftDeletableEntities();

        return base.SaveChangesAsync(cancellation);
    }

    private void UpdateAuditableEntities()
    {
        foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.LastModifiedOnUtc)).CurrentValue = DateTime.UtcNow;
            }
        }
    }

    private void UpdateSoftDeletableEntities()
    {
        foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = DateTime.UtcNow;
                entityEntry.State = EntityState.Modified;
                UpdateDeletedEntityEntryReferencesToUnchaged(entityEntry);
            }
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchaged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
        {
            return;
        }

        foreach (ReferenceEntry reference in entityEntry.References.Where(r => r.TargetEntry!.State == EntityState.Deleted))
        {
            reference.TargetEntry!.State = EntityState.Unchanged;
            UpdateDeletedEntityEntryReferencesToUnchaged(reference.TargetEntry);
        }
    }
}
