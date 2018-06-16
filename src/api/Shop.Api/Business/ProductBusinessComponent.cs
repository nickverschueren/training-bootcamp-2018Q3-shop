using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shop.Api.Data.Model;
using Shop.Api.Data.Repositories;

namespace Shop.Api.Business
{
    public class ProductBusinessComponent : IProductBusinessComponent
    {
        private readonly IProductRepository _productsRepository;
        private readonly string _defaultImageUri;

        public ProductBusinessComponent(IProductRepository productsRepository, IConfiguration configuration)
        {
            _productsRepository = productsRepository;
            _defaultImageUri = configuration.GetSection("products")
                .GetValue<string>("defaultImageUri");
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

        public async Task<(Product product, BusinessErrorCollection errors)> UpdateProductReserved(int id, int quantity)
        {
            var errors = new BusinessErrorCollection();
            var product = await _productsRepository.GetProductById(id);

            // Validate product is found
            if (product == null)
            {
                errors.Add(BusinessErrors.P001ProductNotFound);
                return (null, errors);
            }

            // Other business rules
            if (product.Stock.Total < quantity)
            {
                errors.Add(BusinessErrors.S002TooManyReserved);
            }

            if (!errors.IsValid)
                return (product, errors);

            product.Stock.Reserved = quantity;
            await _productsRepository.UpdateProduct(product);

            return (product, errors);
        }

        private void EnsureImageUri(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ImageUri))
                product.ImageUri = _defaultImageUri;
        }
    }
}
