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

        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<RestrictedWord> RestrictedWords => Set<RestrictedWord>();
    }
}
