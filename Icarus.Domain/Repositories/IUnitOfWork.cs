namespace Icarus.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangeAsync(CancellationToken cancellation = default);
}
