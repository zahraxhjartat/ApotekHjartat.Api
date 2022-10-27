using ApotekHjartat.Common.Exceptions;
using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.Enums;
using ApotekHjartat.DbAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            customerOrder.Created = DateTime.Now;
            //TODO add ordernumber
            var addedOrder = await _context.CustomerOrder.AddAsync(customerOrder);
            await _context.SaveChangesAsync();
            return addedOrder.Entity;
        }

        public async Task<CustomerOrder> GetCustomerOrderById(int id)
        {
            var dbOrder = await _context.CustomerOrder.Include(e => e.CustomerOrderRows).FirstOrDefaultAsync(x => x.CustomerOrderId == id);
            if (dbOrder == null)
            {
                throw new NotFoundException($"No customer order found with id {id}");
            }
            return dbOrder;
        }

        public async Task<CustomerOrder> CancelCustomerOrderById(int id)
        {
            var dbOrder = await _context.CustomerOrder.Include(e => e.CustomerOrderRows).FirstOrDefaultAsync(x => x.CustomerOrderId == id);
            if (dbOrder == null)
            {
                throw new NotFoundException($"No customer order found with id {id}");
            }

            dbOrder.OrderStatus = CustomerOrderStatus.Cancelled;
            await _context.SaveChangesAsync();

            return dbOrder;
        }

        public async Task<(List<CustomerOrder> list, int totalCount)> GetCustomerOrdersByFilter(CustomerOrderFilter filter)
        {
            if (filter == null) return (new List<CustomerOrder>(), 0);

            var query = _context.CustomerOrder
                .Include(e => e.CustomerOrderRows)
                .Where(e =>
                    (filter.OrderNumber == null || filter.OrderNumber == e.OrderNumber) &&
                    (filter.CustomerOrderStatus == null || filter.CustomerOrderStatus.Value == e.OrderStatus) &&
                    (filter.FromDate == null || filter.FromDate <= e.Created) &&
                    (filter.ToDate == null || filter.ToDate >= e.Created)
                        );

            var totalCount = await query.CountAsync();

            var list = await query.Skip(filter.Skip).Take(filter.Take).ToListAsync();
            return (list, totalCount);
        }

        public async Task<CustomerOrder> DeleteCustomerOrderCustomerDataById(int id)
        {
            var dbOrder = await _context.CustomerOrder.Include(e => e.CustomerOrderRows).FirstOrDefaultAsync(x => x.CustomerOrderId == id);
            if (dbOrder == null)
            {
                throw new NotFoundException($"No customer order found with id {id}");
            }

            dbOrder.CustomerAddress = null;
            dbOrder.CustomerEmailAddress = null;
            dbOrder.CustomerFirstName = null;
            dbOrder.CustomerSurname = null;

            await _context.SaveChangesAsync();

            return dbOrder;
        }
    }
    public interface ICustomerOrderDataAccess
    {
        Task<CustomerOrder> CreateCustomerOrder(CustomerOrder customerOrder);
        Task<CustomerOrder> GetCustomerOrderById(int id);
        Task<CustomerOrder> CancelCustomerOrderById(int id);
        Task<(List<CustomerOrder> list, int totalCount)> GetCustomerOrdersByFilter(CustomerOrderFilter filter);
        Task<CustomerOrder> DeleteCustomerOrderCustomerDataById(int id);
    }
}
