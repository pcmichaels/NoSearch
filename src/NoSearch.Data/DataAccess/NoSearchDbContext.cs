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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>().HasData(
                new Tag("Blog", -1),
                new Tag("News", -2),
                new Tag("Programming", -3),
                new Tag("Tutorial", -4),
                new Tag("Video", -5)
            );
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
