using Icarus.Domain.Entity;
using Icarus.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Icarus.Persistence;

public class IcarusDbContext : DbContext
{
    public IcarusDbContext(DbContextOptions<IcarusDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
    }

    public DbSet<Member> Members { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}
