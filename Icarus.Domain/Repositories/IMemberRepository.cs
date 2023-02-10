using Icarus.Domain.Entity;
using Icarus.Domain.ValueObjects;

namespace Icarus.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

    Task<Member?> GetByEmailAsync(string email, CancellationToken cancellation = default);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellation = default);
    void Add(Member member);

    void Update(Member member);
    void Remove(Member member);
}
