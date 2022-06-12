using Microsoft.EntityFrameworkCore;
using NoSearch.Data.Resources;

namespace NoSearch.Data.DataAccess
{
    public class NoSearchDbContext : DbContext
    {
        public NoSearchDbContext(DbContextOptions<NoSearchDbContext> options)
            : base(options)
        {
        }

        public DbSet<Resource>? Resources { get; set; }

    }
}
