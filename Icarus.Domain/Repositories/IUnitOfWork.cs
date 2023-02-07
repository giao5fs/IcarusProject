using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangeAsync(CancellationToken cancellation = default);
    }
}
