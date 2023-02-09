using Icarus.Domain.Entity;
using Icarus.Domain.Repositories;

namespace Icarus.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IcarusDbContext _context;

        public MemberRepository(IcarusDbContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var member = _context.Set<Member>().FirstOrDefault(x => x.Id == id);
            await Task.Delay(100);
            return member;
        }

        public bool IsEmailUniqueAsync(string? email, CancellationToken cancellation = default)
        {
            return !_context.Members.Any(x => x.Email == email);
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

        public async Task<Member?> GetByEmailAsync(string email, CancellationToken cancellation = default)
        {
            var member = await _context.Set<Member>().FindAsync(email, cancellation);
            return member;
        }
    }
}
