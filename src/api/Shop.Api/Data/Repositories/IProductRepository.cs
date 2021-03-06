﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Api.Data.Model;

namespace Shop.Api.Data.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int page = 0, int pageSize = 0, string sort = null);
        Task<int> GetProductCount();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}