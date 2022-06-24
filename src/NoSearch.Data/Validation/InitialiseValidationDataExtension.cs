using Microsoft.EntityFrameworkCore;

namespace NoSearch.Data.Validation
{
    internal static class InitialiseValidationDataExtension
    {
        public static void InitialiseRestrictedWordData(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<RestrictedWord>().HasData(
                    new RestrictedWord("shit", -1),
                    new RestrictedWord("fuck", -2)

                );        
    }
}
