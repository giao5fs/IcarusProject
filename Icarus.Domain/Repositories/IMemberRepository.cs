using Icarus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.Repositories
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

        Task<Member?> GetByEmailAsync(string email, CancellationToken cancellation = default);
        bool IsEmailUniqueAsync(string email, CancellationToken cancellation = default);
        void Add(Member member);

        void Update(Member member);
        void Remove(Member member);
    }
}
