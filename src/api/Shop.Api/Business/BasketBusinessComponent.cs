using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Shop.Api.Data.Model;
using Shop.Api.Data.Repositories;

namespace Shop.Api.Business
{
    public class BasketBusinessComponent : IBasketBusinessComponent
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductBusinessComponent _productBusinessComponent;

        public BasketBusinessComponent(IBasketRepository basketRepository, IProductBusinessComponent productBusinessComponent)
        {
            _basketRepository = basketRepository;
            _productBusinessComponent = productBusinessComponent;
        }

        public Task<Basket> GetBasketByUserId(string userId)
        {
            return _basketRepository.GetBasketByUserId(userId);
        }

        public async Task<(Basket basket, BusinessErrorCollection errors)> AddProductToBasket(string userId, int productId, int quantity)
        {
            var product = await _productBusinessComponent.GetProductById(productId);
            var errors = new BusinessErrorCollection();
            var newBasket = false;

            if (product == null) return (null, null);

            if (product.Stock.Total - product.Stock.Reserved < quantity)
            {
                errors.Add("S001", "Not enough products in stock");
                return (null, errors);
            }

            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket == null)
            {
                newBasket = true;
                basket = new Basket
                {
                    UserId = userId,
                    Items = new List<BasketItem>()
                };
            }

            var item = basket.Items.SingleOrDefault(i => i.ProductId == productId);

            //using (var scope = new TransactionScope())
            {
                if (newBasket)
                    await _basketRepository.AddBasket(basket);
                else
                    await _basketRepository.UpdateBasket(basket);

                if (item == null)
                {

                    item = new BasketItem
                    {
                        BasketId = basket.Id,
                        ProductId = productId,
                        Quantity = quantity
                    };
                    await _basketRepository.AddBasketItem(item);
                }
                else
                {
                    item.Quantity += quantity;
                    await _basketRepository.UpdateBasketItem(item);
                }

                product.Stock.Reserved += quantity;
                await _productBusinessComponent.UpdateProduct(product);

                //scope.Complete();
            }

            return (basket, errors);
        }
    }
}
