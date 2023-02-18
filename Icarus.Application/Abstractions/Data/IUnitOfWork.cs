namespace Icarus.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task SaveChangeAsync(CancellationToken cancellation = default);
}
