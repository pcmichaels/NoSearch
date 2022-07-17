using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data.Resources
{
    internal static class InitialResourceDataExtension
    {
        public static void InitialiseTagData(this ModelBuilder modelBuilder) =>        
            modelBuilder.Entity<Tag>().HasData(
                new Tag("Blog", -1),
                new Tag("News", -2),
                new Tag("Programming", -3),
                new Tag("Tutorial", -4),
                new Tag("Video", -5),
                new Tag("Online Tools", -6),
                new Tag("Online Games", -7),
                new Tag("Dissertation / White Paper", -8)
            );        
    }
}
