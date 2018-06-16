using Shop.Api.Helpers;

namespace Shop.Api.Api.Model.Mapping
{
    public class ProductSortExpressionMapper : SortExpressionMapper<Product, Data.Model.Product>, IProductSortExpressionMapper
    {
        public ProductSortExpressionMapper()
        {
            SetMapping(nameof(Product.Stock), p => p.Stock.Total);
            SetMapping(nameof(Product.Desc), p => p.Description);
            SetMapping(nameof(Product.Reserved), p => p.Stock.Reserved);
        }
    }
}