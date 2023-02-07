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
        public void Add(Member member)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var member = await _context.Set<Member>().FindAsync(id, cancellation);
            return member;
        }

        public void Remove(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
