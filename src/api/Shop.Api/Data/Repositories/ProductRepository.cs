using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Data.Model;

namespace Shop.Api.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ShopDbContext shopDbContext)
        {
            ShopDbContext = shopDbContext;
        }

        private ShopDbContext ShopDbContext { get; }

        public Task<int> GetProductCount()
        {
            return ShopDbContext.Products.CountAsync();
        }

        public Task<List<Product>> GetProducts(int page = 0, int pageSize = 0)
        {
            IQueryable<Product> query = ShopDbContext.Products.Include(p => p.Stock);

            if (pageSize > 0)
            {
                query = query.Skip(pageSize * page).Take(pageSize);
            }

            return query.OrderBy(p => p.Id).ToListAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            return ShopDbContext.Products.FindAsync(id);
        }
    }
}
