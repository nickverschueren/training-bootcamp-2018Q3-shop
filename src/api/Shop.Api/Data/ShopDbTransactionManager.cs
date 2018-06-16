using Shop.Api.Data.Model;
using Shop.Api.Helpers;

namespace Shop.Api.Data
{
    public class ShopDbTransactionManager : TransactionManager<ShopDbContext>, IShopDbTransactionManager
    {
        public ShopDbTransactionManager(ShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}