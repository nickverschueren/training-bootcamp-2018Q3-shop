using System.Threading.Tasks;
using Shop.Api.Data.Model;

namespace Shop.Api.Data.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketByUserId(string userId);
        Task DeleteBasket(Basket basket);

        Task AddBasket(Basket basket);
        Task UpdateBasket(Basket basket);
        Task AddBasketItem(BasketItem item);
        Task UpdateBasketItem(BasketItem item);
        Task DeleteBasketItem(BasketItem item);
    }
}