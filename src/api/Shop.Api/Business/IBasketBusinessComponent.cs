using System.Threading.Tasks;
using Shop.Api.Data.Model;

namespace Shop.Api.Business
{
    public interface IBasketBusinessComponent
    {
        Task<Basket> GetBasketByUserId(string userId);
        Task<(Basket basket, BusinessErrorCollection errors)> AddProductToBasket(
            string userId, int productId, int itemQuantityQuantity);
    }
}