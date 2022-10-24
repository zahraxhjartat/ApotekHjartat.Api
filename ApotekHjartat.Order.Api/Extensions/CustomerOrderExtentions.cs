using ApotekHjartat.Order.Api.Models;
using ApotekHjartat.Order.DbAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekHjartat.Order.Api.Extensions
{
    public static class CustomerOrderExtentions
    {
        public static CustomerOrder ToDbModel(this CustomerOrderDto from)
        {
            return new CustomerOrder()
            {
                OrderNumber = from.OrderNumber
            };
        }

        public static CustomerOrderDto ToDto(this CustomerOrder from)
        {
            return new CustomerOrderDto()
            {
                CustomerOrderId = from.CustomerOrderId,
                OrderNumber = from.OrderNumber
            };
        }
    }
}
