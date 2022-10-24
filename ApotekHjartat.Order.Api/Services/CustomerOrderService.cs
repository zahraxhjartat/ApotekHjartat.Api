using ApotekHjartat.Order.Api.Models;
using ApotekHjartat.Order.Api.Extensions;
using ApotekHjartat.Order.DbAccess.DataAccess.v1;
using System;
using System.Threading.Tasks;

namespace ApotekHjartat.Order.Api.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderDataAccess _customerOrderDataAccess;
        public CustomerOrderService(ICustomerOrderDataAccess customerOrderDataAccess)
        {
            _customerOrderDataAccess = customerOrderDataAccess ?? throw new ArgumentNullException(nameof(customerOrderDataAccess));
        }

        public async Task<CustomerOrderDto> CreateCustomerOrder(CustomerOrderDto data)
        {
            var dbOrder = await _customerOrderDataAccess.CreateCustomerOrder(data.ToDbModel());
            return dbOrder.ToDto();
        }

        public async Task<CustomerOrderDto> GetCustomerOrderById(int id)
        {
            var dbCustomerOrder = await _customerOrderDataAccess.GetCustomerOrderById(id);
            return dbCustomerOrder?.ToDto();
        }

    }

    public interface ICustomerOrderService
    {
        Task<CustomerOrderDto> CreateCustomerOrder(CustomerOrderDto data);
        Task<CustomerOrderDto> GetCustomerOrderById(int id);
 
    }
}
