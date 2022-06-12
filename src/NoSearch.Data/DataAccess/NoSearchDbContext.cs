using Microsoft.EntityFrameworkCore;
using NoSearch.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data.DataAccess
{
    public class NoSearchDbContext : DbContext
    {
        public NoSearchDbContext(DbContextOptions<NoSearchDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Resource> Resources { get; set; }
        
    }
}
