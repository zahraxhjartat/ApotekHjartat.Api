using ApotekHjartat.Api.Models;
using ApotekHjartat.Api.Extensions;
using System;
using System.Threading.Tasks;
using ApotekHjartat.Api.Models.v1;
using ApotekHjartat.DbAccess.DataAccess;
using ApotekHjartat.Common.Exceptions;

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
            return dbOrder.ToDto();
        }

        public async Task<CustomerOrderDto> GetCustomerOrderById(int id)
        {
            var dbCustomerOrder = await _customerOrderDataAccess.GetCustomerOrderById(id);

            if(dbCustomerOrder == null)
            {
                throw new NotFoundException($"Could not find customer order with id {id}");
            }

            return dbCustomerOrder.ToDto();
        }
    }

    public interface ICustomerOrderService
    {
        Task<CustomerOrderDto> CreateCustomerOrder(AddCustomerOrderDto data);
        Task<CustomerOrderDto> GetCustomerOrderById(int id);
    }
}
