using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Api.Data.Model;
using Shop.Api.Data.Repositories;

namespace Shop.Api.Business
{
    public class ProductBusinessComponent : IProductBusinessComponent
    {
        private readonly IProductRepository _productsRepository;

        public ProductBusinessComponent(IProductRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Task<List<Product>> GetProducts()
        {
            return _productsRepository.GetProducts();
        }

        public async Task<(List<Product> items, int total)> GetProductsPaged(int page, int pageSize)
        {
            var items = await _productsRepository.GetProducts(page, pageSize);
            var total = await _productsRepository.GetProductCount();
            return (items, total);
        }

        public Task<Product> GetProductById(int id)
        {
            return _productsRepository.GetProductById(id);
        }
    }
}
