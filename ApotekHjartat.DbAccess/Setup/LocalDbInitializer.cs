using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ApotekHjartat.DbAccess.Setup
{
    public static class LocalDbInitializer
    {
        public static void SetupLocalDb(this OrderDbContext context)
        {
            // Do not migrate when testing
            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            context.SeedData();

        }
    }
}
