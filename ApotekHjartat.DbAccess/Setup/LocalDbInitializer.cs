using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekHjartat.DbAccess.Setup
{
    public static class LocalDbInitializer
    {
        public static async Task SetupLocalDb(this OrderDbContext context)
        {
            // Do not migrate when testing
            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            if (context.CustomerOrder.Any()) return;

            try
            {
                await SeedData(context);
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
            var orderRows = mockData.GetCustomerOrderRows();
            context.Product.AddRange(products);
            context.CustomerOrder.AddRange(orders);
            context.CustomerOrderRow.AddRange(orderRows);
            await context.SaveChangesAsync();
        }
    }
}
