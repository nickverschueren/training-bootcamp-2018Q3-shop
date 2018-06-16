using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Api.Model.Mapping;
using Shop.Api.Data.Model;

namespace Shop.Api.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _shopDbContext;
        private readonly IProductSortExpressionMapper _productSortExpressionMapper;

        public ProductRepository(ShopDbContext shopDbContext, IProductSortExpressionMapper productSortExpressionMapper)
        {
            _shopDbContext = shopDbContext;
            _productSortExpressionMapper = productSortExpressionMapper;
        }

        public Task<int> GetProductCount()
        {
            return _shopDbContext.Products.CountAsync();
        }

        public Task<List<Product>> GetProducts(int page = 0, int pageSize = 0, string sort = null)
        {
            IQueryable<Product> query = _shopDbContext.Products.Include(p => p.Stock);
            query = _productSortExpressionMapper.ApplySortExpression(query, sort, p => p.Id);

            if (pageSize > 0)
            {
                query = query.Skip(pageSize * page).Take(pageSize);
            }

            return query.ToListAsync();
        }

        public Task<Product> GetProductById(int id)
        {
            return _shopDbContext.Products.FindAsync(id);
        }

        public async Task AddProduct(Product product)
        {
            await _shopDbContext.Products.AddAsync(product);
            await _shopDbContext.SaveChangesAsync();
        }

        public Task UpdateProduct(Product product)
        {
            _shopDbContext.Products.Attach(product).State = EntityState.Modified;
            return _shopDbContext.SaveChangesAsync();
        }

        public Task DeleteProduct(Product product)
        {
            _shopDbContext.Products.Remove(product);
            return _shopDbContext.SaveChangesAsync();
        }
    }
}
