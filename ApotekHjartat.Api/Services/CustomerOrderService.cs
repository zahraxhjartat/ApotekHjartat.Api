using ApotekHjartat.Api.Models;
using ApotekHjartat.Api.Extensions;
using System;
using System.Threading.Tasks;
using ApotekHjartat.Api.Models.v1;
using ApotekHjartat.DbAccess.DataAccess;
using ApotekHjartat.Common.Exceptions;
using ApotekHjartat.DbAccess.Enums;
using System.Collections.Generic;
using System.Linq;
using ApotekHjartat.DbAccess.Models;

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

        public async Task<PaginatedResponseDto<CustomerOrderDto>> GetCustomerOrdersByFilter(CustomerOrderFilterDto filter)
        {

            var response = new PaginatedResponseDto<CustomerOrderDto>();
            List<CustomerOrder> list;

            (list, response.TotalCount) = await _customerOrderDataAccess.GetCustomerOrdersByFilter(filter.ToDbModel());
            response.Page = list.Select(x => x.ToDto()).ToList();

            if (response.Page.Count < filter.Take)
            {
                response.Next = null;
            }
            else
            {
                response.Next = response.Page.Count + filter.Skip;
            }

            return response;
        }

        public async Task<CustomerOrderDto> DeleteCustomerOrderCustomerDataById(int id)
        {

            var statusesByWhichOrderIsAllowedToBeCancelled = new List<CustomerOrderStatus> { CustomerOrderStatus.Cancelled,
                                                                                             CustomerOrderStatus.Refunded,
                                                                                             CustomerOrderStatus.Archived};
            var dbCustomerOrder = await _customerOrderDataAccess.GetCustomerOrderById(id);
            if (dbCustomerOrder == null)
            {
                throw new NotFoundException($"Could not find customer order with id {id}");
            }

            if (!statusesByWhichOrderIsAllowedToBeCancelled.Contains(dbCustomerOrder.OrderStatus))
            {
                throw new NotAllowedException($"Order is not allowed to lose customer data yet");
            }

            var updatedOrder = await _customerOrderDataAccess.DeleteCustomerOrderCustomerDataById(id);

            return updatedOrder.ToDto();
        }
    }

    public interface ICustomerOrderService
    {
        /// <summary>
        /// Create customer order
        /// </summary>
        Task<CustomerOrderDto> CreateCustomerOrder(AddCustomerOrderDto data);
        /// <summary>
        /// Get customer order by id
        /// </summary>
        Task<CustomerOrderDto> GetCustomerOrderById(int id);
        /// <summary>
        /// Cancel customer order by id if it is inactive
        /// </summary>
        Task<CustomerOrderDto> CancelCustomerOrderById(int id);
        /// <summary>
        /// Get customer order by filter
        /// </summary>
        Task<PaginatedResponseDto<CustomerOrderDto>> GetCustomerOrdersByFilter(CustomerOrderFilterDto filter);
        /// <summary>
        /// Delete customer data on customer order if status is inactive
        /// </summary>
        Task<CustomerOrderDto> DeleteCustomerOrderCustomerDataById(int id);
    }
}
