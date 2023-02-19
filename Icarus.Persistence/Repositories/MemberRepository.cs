using Icarus.Application.Abstractions.Data;
using Icarus.Domain.Entities;
using Icarus.Domain.Repositories;
using Icarus.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Icarus.Persistence.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly IDbContext _context;

    public MemberRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellation)
    {
        var member = await _context.Set<Member>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellation);
        return member;
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellation = default)
    {
        return !await _context.Set<Member>().AnyAsync(x => x.Email == email.Value, cancellationToken: cancellation);
    }

    public async Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellation = default)
    {
        var member = await _context.Set<Member>().FirstOrDefaultAsync(x => x.Email == email.Value, cancellationToken: cancellation);
        return member;
    }
    public void Add(Member member)
    {
        _context.Set<Member>().Add(member);
    }

    public void Update(Member member)
    {
        _context.Set<Member>().Update(member);
    }
    public void Remove(Member member)
    {
        _context.Set<Member>().Remove(member);
    }
}
