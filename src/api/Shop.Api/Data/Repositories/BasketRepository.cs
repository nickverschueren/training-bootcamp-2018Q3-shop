using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Data.Model;

namespace Shop.Api.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShopDbContext _shopDbContext;

        public BasketRepository(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public Task<Basket> GetBasketByUserId(string userId)
        {
            return _shopDbContext.Baskets
                .Include(b => b.Items)
                .SingleOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task AddBasket(Basket basket)
        {
            await _shopDbContext.Baskets.AddAsync(basket);
            await _shopDbContext.SaveChangesAsync();
        }

        public Task UpdateBasket(Basket basket)
        {
            _shopDbContext.Baskets.Attach(basket).State = EntityState.Modified;
            return _shopDbContext.SaveChangesAsync();
        }

        public async Task AddBasketItem(BasketItem item)
        {
            await _shopDbContext.BasketItems.AddAsync(item);
            await _shopDbContext.SaveChangesAsync();
        }

        public Task UpdateBasketItem(BasketItem item)
        {
            _shopDbContext.BasketItems.Attach(item).State = EntityState.Modified;
            return _shopDbContext.SaveChangesAsync();
        }
    }
}
