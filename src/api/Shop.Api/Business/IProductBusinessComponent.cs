using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Api.Data.Model;

namespace Shop.Api.Business
{
    public interface IProductBusinessComponent
    {
        Task<List<Product>> GetProducts();
        Task<(List<Product> items, int total)> GetProductsPaged(int page, int pageSize);
        Task<Product> GetProductById(int id);
    }
}