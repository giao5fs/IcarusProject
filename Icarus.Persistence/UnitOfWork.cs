using Icarus.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Persistence
{
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
}
