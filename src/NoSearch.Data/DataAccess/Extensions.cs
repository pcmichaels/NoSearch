using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data.DataAccess
{
    public static class Extensions
    {
        public static void ConfigureServices(this IServiceCollection services, string connectionString) =>        
            services.AddDbContext<NoSearchDbContext>(a =>
                a.UseSqlServer(connectionString));

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<NoSearchDbContext>();
            if (context == null) throw new Exception("Unable to get context");

            context.Database.Migrate();
        }
    }
}
