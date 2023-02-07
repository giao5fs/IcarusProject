using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Persistence
{
    public class IcarusDbContext : DbContext
    {
        public IcarusDbContext(DbContextOptions<IcarusDbContext> options): base(options)
        {

        }
    }
}
