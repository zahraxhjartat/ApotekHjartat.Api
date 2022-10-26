using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekHjartat.DbAccess.DataAccess
{
    public class ProductDataAccess : IProductDataAccess
    {
        private readonly OrderDbContext _context;
        public ProductDataAccess(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Product>> GetProductsByIds(List<int> idList)
        {
           var dbProducts = _context.Product.Where(e => idList.Contains(e.ProductId));
           return dbProducts.ToList();
        }
    }

    public interface IProductDataAccess
    {
        Task<List<Product>> GetProductsByIds(List<int> idList);
    }
}
