using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Api.Data.Model;

namespace Shop.Api.Business
{
    public interface IProductBusinessComponent
    {
        Task<List<Product>> GetProducts(string sort);
        Task<(List<Product> items, int total)> GetProductsPaged(int page, int pageSize, string sort);
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<(Product product, BusinessErrorCollection errors)>
            UpdateProductReserved(int id, int quantity);
    }
}