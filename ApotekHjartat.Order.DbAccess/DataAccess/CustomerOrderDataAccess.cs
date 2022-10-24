using ApotekHjartat.Order.Common.Exceptions;
using ApotekHjartat.Order.DbAccess.Context;
using ApotekHjartat.Order.DbAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ApotekHjartat.Order.DbAccess.DataAccess.v1
{
    public class CustomerOrderDataAccess : ICustomerOrderDataAccess
    {
        private readonly OrderDbContext _context;
        public CustomerOrderDataAccess(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<CustomerOrder> CreateCustomerOrder(CustomerOrder customerOrder)
        {
            var addedOrder = await _context.CustomerOrder.AddAsync(customerOrder);
            await _context.SaveChangesAsync();
            return addedOrder.Entity;
        }

        public async Task<CustomerOrder> GetCustomerOrderById(int id)
        {
            var result = await _context.CustomerOrder.FirstOrDefaultAsync(x => x.CustomerOrderId == id);
            if(result == null)
            {
                throw new NotFoundException($"No customer order found with id {id}");
            }
            return result;
        }
    }
    public interface ICustomerOrderDataAccess
    {
        Task<CustomerOrder> CreateCustomerOrder(CustomerOrder customerOrder);
        Task<CustomerOrder> GetCustomerOrderById(int id);
    }
}
