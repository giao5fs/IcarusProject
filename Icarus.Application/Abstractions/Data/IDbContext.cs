using Icarus.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Icarus.Application.Abstractions.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

    Task<TEntity?> GetByIdAsync<TEntity>(Guid id) where TEntity : Entity;

    void Insert<TEntity>(TEntity entity) where TEntity : Entity;

    void Remove<TEntity>(TEntity entity) where TEntity : Entity;
}
