using Icarus.Domain.Repositories;

namespace Icarus.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IcarusDbContext _context;

    public UnitOfWork(IcarusDbContext context)
    {
        _context = context;
    }

    public Task SaveChangeAsync(CancellationToken cancellation = default)
    {
        return _context.SaveChangesAsync(cancellation);
    }
}
