using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.Setup;

namespace ApotekHjartat.DbAccess.Extentions
{
    public static class OrderDbContextExtentions
    {
        public static void SeedData(this OrderDbContext context)
        {
            var mockData = new MockDataFactory();
            var products = mockData.GetProducts();
            var orders = mockData.GetCustomerOrders();
            var orderRows = mockData.GetCustomerOrderRows();
            context.Product.AddRange(products);
            context.CustomerOrder.AddRange(orders);
            context.SaveChanges();
            context.CustomerOrderRow.AddRange(orderRows);
            context.SaveChangesAsync();
        }
    }
}
