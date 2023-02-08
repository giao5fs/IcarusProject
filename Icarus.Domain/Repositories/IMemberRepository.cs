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
        Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellation = default);
        void Add(Member member);
        void Remove(Member member);
    }
}
