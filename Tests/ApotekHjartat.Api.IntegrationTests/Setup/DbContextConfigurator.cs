using ApotekHjartat.DbAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
namespace ApotekHjartat.Api.IntegrationTest.Setup
{
    public class DbContextConfigurator
    {
        public static OrderDbContext Context { get; private set; }
        public static string ConnString => @$"Server=(localdb)\mssqllocaldb;Database=ApotekHjartatApiIntegrationTest;Trusted_Connection=True;MultipleActiveResultSets=true";
        internal static void DeleteDatabase()
        {
            if (Context != null)
            {
                Context.Database.EnsureDeleted();
            }
        }
        public static void InitDatabase()
        {
            if (Context == null)
            {
                var options = new DbContextOptionsBuilder<OrderDbContext>()
                    .UseSqlServer(ConnString)
                    .Options;
                Context = new OrderDbContext(options);
                Context.Database.EnsureDeleted();
                Context.Database.Migrate();
            }
            else
            {
                if (!Context.Database.CanConnect())
                {
                    throw new Exception("Unable to connect to database");
                }
            }
        }
    }
}