using Microsoft.EntityFrameworkCore;
using NoSearch.Data.Resources;
using NoSearch.Data.Validation;

namespace NoSearch.Data.DataAccess
{
    public class NoSearchDbContext : DbContext
    {
        public NoSearchDbContext(DbContextOptions<NoSearchDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.InitialiseTagData();
            modelBuilder.InitialiseRestrictedWordData();
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RestrictedWord> RestrictedWords { get; set; }
    }
}
