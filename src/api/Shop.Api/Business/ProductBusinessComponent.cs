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

        public Task AddProduct(Product product)
        {
            EnsureImageUri(product);
            return _productsRepository.AddProduct(product);
        }

        public Task UpdateProduct(Product product)
        {
            EnsureImageUri(product);
            return _productsRepository.UpdateProduct(product);
        }

        public Task DeleteProduct(Product product)
        {
            return _productsRepository.DeleteProduct(product);
        }

        private static void EnsureImageUri(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ImageUri))
                product.ImageUri = "https://dummyimage.com/300x300.jpg";
        }
    }
}
