using ApotekHjartat.DbAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ApotekHjartat.DbAccess.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }
        public virtual DbSet<CustomerOrderRow> CustomerOrderRow { get; set; }
    }
}
