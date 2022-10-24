using ApotekHjartat.Order.DbAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekHjartat.Order.DbAccess.Context
{
    public class OrderDbContext : DbContext
    {
  public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }
        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }
    }
}
