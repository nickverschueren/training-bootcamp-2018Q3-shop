using System.Threading.Tasks;
using Shop.Api.Data.Model;

namespace Shop.Api.Business
{
    public interface IBasketBusinessComponent
    {
        Task<Basket> GetBasketByUserId(string userId);
        Task<Basket> DeleteBasketByUserId(string userId);

        Task<(Basket basket, BusinessErrorCollection errors)>
            AddItemToBasket(string userId, int productId, int itemQuantityQuantity);
        Task<(Basket basket, BusinessErrorCollection errors)>
            UpdateItemInBasket(string userId, int id, int itemQuantityQuantity);
        Task<(Basket basket, BusinessErrorCollection errors)>
            DeleteItemInBasket(string userId, int id);
    }
}