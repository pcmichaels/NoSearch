using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace NoSearch.Data.DataAccess
{
    public static class Extensions
    {
        public static void ConfigureServices(this IServiceCollection services,
                                             string connectionString) =>
            services.AddDbContext<NoSearchDbContext>(a =>
                a.UseSqlServer(connectionString));

        public static void UpdateDatabase(this IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder
                .ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<NoSearchDbContext>();
            if (context == null)
            {
                throw new Exception("Unable to get data context");
            }

            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
            context.Database.Migrate();
        }
    }
}
}
