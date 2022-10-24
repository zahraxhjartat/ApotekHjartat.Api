using ApotekHjartat.Order.DbAccess.Context;
using ApotekHjartat.Order.DbAccess.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApotekHjartat.Order.DbAccess.Extentions
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
            context.SaveChanges();

            foreach (var orderRow in orderRows)
            {
                foreach (var product in products)
                {
                    if (orderRow.ProductId == product.ProductId)
                    {
                        orderRow.ProductName = product.Name;
                        orderRow.PriceExclVat = product.PriceExclVat * orderRow.OrderedAmount;
                        orderRow.Vat = product.Vat;
                        orderRow.IsRx = product.VaraArticleType == "RX";
                    }
                }
            }
            context.CustomerOrder.AddRange(orders);
            context.CustomerOrderRow.AddRange(orderRows);
            context.SaveChangesAsync();
        }
    }
}
