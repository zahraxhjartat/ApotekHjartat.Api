using ApotekHjartat.DbAccess.Models;
using System;
using System.Collections.Generic;

namespace ApotekHjartat.DbAccess.Setup
{
    internal class MockDataFactory
    {
        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Zyprexa Velotab 15 mg 28 styck Frystorkad tablett",
                    VaraArticleType = "RX",
                    PriceExclVat = 662M,
                    Vat = 0M
                },
                new Product
                {
                    Name = "Cortimyk 20 mg/g + 10 mg/g 50 gram Kräm",
                    VaraArticleType = "RX",
                    PriceExclVat = 103M
                },
                new Product
                {
                    Name = "Propyless 200 mg/g 480 gram Kutan emulsion",
                    VaraArticleType = "EX",
                    PriceExclVat = 189M,
                    Vat = 0.25M
                },
                new Product
                {
                    Name = "ACO SUN LOTION SPF20",
                    VaraArticleType = "",
                    PriceExclVat = 103,
                    Vat = 0.25M
                },
                new Product
                {
                    Name = "IsaDora Brow Shaping Gel Transparent",
                    VaraArticleType = "",
                    PriceExclVat = 79M,
                    Vat = 0.25M
                }
            };
        }

        public List<CustomerOrder> GetCustomerOrders()
        {
            return new List<CustomerOrder>
            {
                new CustomerOrder
                {
                    OrderNumber = "1",
                    Created = DateTime.Now,
                    CustomerEmailAddress = "darthmaul@gmail.com",
                    CustomerFirstName = "Darth",
                    CustomerSurname = "Maul",
                    CustomerAddress = "Dathomir Street 11",
                    CustomerOrderRows = new List<CustomerOrderRow>()
                    {
                       new CustomerOrderRow
                       {
                          CustomerOrderId = 1,
                          ProductId = 1,
                          OrderedAmount = 1,
                          PriceExclVat = 662M,
                          Vat = 0M
                       },
                       new CustomerOrderRow
                       {
                          CustomerOrderId = 1,
                          ProductId = 2,
                          OrderedAmount = 1,
                          PriceExclVat = 103M
                       },
                       new CustomerOrderRow
                       {
                         CustomerOrderId = 1,
                         ProductId = 4,
                         OrderedAmount = 2,
                         PriceExclVat = 103,
                         Vat = 0.25M
                       }
                    }
                },
                new CustomerOrder
                {
                    OrderNumber = "2",
                    Created = DateTime.Now,
                    CustomerEmailAddress = "chewbacca@gmail.com",
                    CustomerFirstName = "Chewbacca",
                    CustomerSurname = "von Kashyyyk",
                    CustomerAddress = "Kashyyyk Street 1",
                    CustomerOrderRows = new List<CustomerOrderRow>()
                    {
                        new CustomerOrderRow
                        {
                            CustomerOrderId = 2,
                            ProductId = 2,
                            OrderedAmount = 1,
                            PriceExclVat = 103M
                        },
                        new CustomerOrderRow
                        {
                            CustomerOrderId = 2,
                            ProductId = 3,
                            OrderedAmount = 1,
                            PriceExclVat = 189M,
                            Vat = 0.25M
                        },
                        new CustomerOrderRow
                        {
                           CustomerOrderId = 2,
                           ProductId = 5,
                           OrderedAmount = 1,
                           PriceExclVat = 79M,
                           Vat = 0.25M
                        }
                    }
                }
            };
        }

    }
}
