using ApotekHjartat.Common.Exceptions;
using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ApotekHjartat.DbAccess.DataAccess
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
