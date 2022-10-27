using ApotekHjartat.DbAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

            if (context.CustomerOrder.Any()) return;

            try
            {
                SeedData(context).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static async Task SeedData(OrderDbContext context)
        {
            var mockData = new MockDataFactory();
            var products = mockData.GetProducts();
            var orders = mockData.GetCustomerOrders();
            context.Product.AddRange(products);
            context.CustomerOrder.AddRange(orders);
            await context.SaveChangesAsync();
        }
    }
}
