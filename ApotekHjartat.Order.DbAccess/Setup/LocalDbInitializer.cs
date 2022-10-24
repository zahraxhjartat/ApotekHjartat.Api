﻿using ApotekHjartat.Order.DbAccess.Context;
using ApotekHjartat.Order.DbAccess.Extentions;
using Microsoft.EntityFrameworkCore;

namespace ApotekHjartat.Order.DbAccess.Setup
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
