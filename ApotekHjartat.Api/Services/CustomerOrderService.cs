using ApotekHjartat.Api.Models;
using ApotekHjartat.Api.Extensions;
using System;
using System.Threading.Tasks;
using ApotekHjartat.Api.Models.v1;
using ApotekHjartat.DbAccess.DataAccess;
using ApotekHjartat.Common.Exceptions;
using ApotekHjartat.DbAccess.Enums;
using System.Collections.Generic;

namespace ApotekHjartat.Api.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderDataAccess _customerOrderDataAccess;
        public CustomerOrderService(ICustomerOrderDataAccess customerOrderDataAccess)
        {
            _customerOrderDataAccess = customerOrderDataAccess ?? throw new ArgumentNullException(nameof(customerOrderDataAccess));
        }

        public async Task<CustomerOrderDto> CreateCustomerOrder(AddCustomerOrderDto data)
        {
            var dbOrder = await _customerOrderDataAccess.CreateCustomerOrder(data.ToDbModel());
            return dbOrder.ToClassifiedDto();
        }

        public async Task<CustomerOrderDto> GetCustomerOrderById(int id)
        {
            var dbCustomerOrder = await _customerOrderDataAccess.GetCustomerOrderById(id);

            if (dbCustomerOrder == null)
            {
                throw new NotFoundException($"Could not find customer order with id {id}");
            }

            return dbCustomerOrder.ToDto();
        }

        public async Task<CustomerOrderDto> CancelCustomerOrderById(int id)
        {
            var statusesByWhichOrderIsAllowedToBeCancelled = new List<CustomerOrderStatus> { CustomerOrderStatus.NotYetProccessed,
                                                                                       CustomerOrderStatus.ReadyForPicking,
                                                                                       CustomerOrderStatus.Processing,
                                                                                       CustomerOrderStatus.Approved };

            var dbCustomerOrder = await _customerOrderDataAccess.GetCustomerOrderById(id);
            if (dbCustomerOrder == null)
            {
                throw new NotFoundException($"Could not find customer order with id {id}");
            }

            if (!statusesByWhichOrderIsAllowedToBeCancelled.Contains(dbCustomerOrder.OrderStatus))
            {
                throw new NotAllowedException($"Order is not allowed to be cancelled");
            }

            var updatedOrder = await _customerOrderDataAccess.CancelCustomerOrderById(id);
            //TODO: trigger refund of payment

            return updatedOrder.ToDto();
        }
    }

    public interface ICustomerOrderService
    {
        Task<CustomerOrderDto> CreateCustomerOrder(AddCustomerOrderDto data);
        Task<CustomerOrderDto> GetCustomerOrderById(int id);
        Task<CustomerOrderDto> CancelCustomerOrderById(int id);
    }
}
