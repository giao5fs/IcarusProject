using Icarus.Domain.Entities;
using Icarus.Domain.Repositories;
using Icarus.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Icarus.Persistence.Repositories;
public class CacheMemberRepository : IMemberRepository
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMemoryCache _memCache;

    public CacheMemberRepository(IMemberRepository memberRepository, IMemoryCache memCache)
    {
        _memberRepository = memberRepository;
        _memCache = memCache;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        string key = $"members-{id}";
        return await _memCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return _memberRepository.GetByIdAsync(id, cancellation);
            });
    }

    public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellation = default)
    {
        return _memberRepository.IsEmailUniqueAsync(email, cancellation);
    }

    public void Remove(Member member)
    {
        _memberRepository.Remove(member);
    }

    public void Update(Member member)
    {
        _memberRepository.Update(member);
    }

    public void Add(Member member)
    {
        _memberRepository.Add(member);
    }

    public Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellation = default)
    {
        return _memberRepository.GetByEmailAsync(email, cancellation);
    }
}