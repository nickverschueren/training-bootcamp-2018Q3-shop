using Shop.Api.Data.Model;

namespace Shop.Api.Data
{
    public class ShopDbTransactionManager : TransactionManager<ShopDbContext>, IShopDbTransactionManager
    {
        public ShopDbTransactionManager(ShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}