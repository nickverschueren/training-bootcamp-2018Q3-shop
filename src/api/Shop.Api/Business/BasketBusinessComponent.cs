using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Api.Data;
using Shop.Api.Data.Model;
using Shop.Api.Data.Repositories;

namespace Shop.Api.Business
{
    public class BasketBusinessComponent : IBasketBusinessComponent
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductBusinessComponent _productBusinessComponent;
        private readonly IShopDbTransactionManager _shopDbTransactionManager;

        public BasketBusinessComponent(IBasketRepository basketRepository, IProductBusinessComponent productBusinessComponent,
            IShopDbTransactionManager shopDbTransactionManager)
        {
            _basketRepository = basketRepository;
            _productBusinessComponent = productBusinessComponent;
            _shopDbTransactionManager = shopDbTransactionManager;
        }

        public Task<Basket> GetBasketByUserId(string userId)
        {
            return _basketRepository.GetBasketByUserId(userId);
        }

        public async Task<Basket> DeleteBasketByUserId(string userId)
        {
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket != null)
                await _basketRepository.DeleteBasket(basket);
            return basket;
        }

        public async Task<(Basket basket, BusinessErrorCollection errors)> AddItemToBasket(string userId, int productId, int quantity)
        {
            var product = await _productBusinessComponent.GetProductById(productId);
            var errors = new BusinessErrorCollection();
            var newBasket = false;

            // Validate product is found
            if (product == null)
            {
                errors.Add(BusinessErrors.P001ProductNotFound);
                return (null, errors);
            }

            // Other business rules
            if (product.Stock.Total - product.Stock.Reserved < quantity)
            {
                errors.Add(BusinessErrors.S001InsufficientStock);
            }

            var basket = await _basketRepository.GetBasketByUserId(userId);

            if (!errors.IsValid)
                return (basket, errors);

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

            try
            {
                await _shopDbTransactionManager.BeginTransaction();

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

                await _shopDbTransactionManager.CommitTransaction();
            }
            catch
            {
                await _shopDbTransactionManager.RollbackTransaction();
                throw;
            }

            return (basket, errors);
        }

        public async Task<(Basket basket, BusinessErrorCollection errors)> UpdateItemInBasket(string userId, int productId, int quantity)
        {
            var product = await _productBusinessComponent.GetProductById(productId);
            var errors = new BusinessErrorCollection();

            // Validate product is found
            if (product == null)
            {
                errors.Add(BusinessErrors.P001ProductNotFound);
                return (null, errors);
            }

            // Validate basket is found
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket == null)
            {
                errors.Add(BusinessErrors.B001BasketNotFound);
                return (null, errors);
            }

            // Validate product is present in basket
            var item = basket.Items.SingleOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                errors.Add(BusinessErrors.I001BasketItemNotFound);
                return (null, errors);
            }

            // Other business rules
            if (product.Stock.Total - product.Stock.Reserved < quantity)
            {
                errors.Add(BusinessErrors.S001InsufficientStock);
            }

            if (!errors.IsValid)
                return (basket, errors);

            try
            {
                await _shopDbTransactionManager.BeginTransaction();

                if (quantity == 0)
                {
                    await _basketRepository.DeleteBasketItem(item);
                }
                else
                {
                    item.Quantity += quantity;
                    await _basketRepository.UpdateBasketItem(item);
                }

                await _shopDbTransactionManager.CommitTransaction();
            }
            catch
            {
                await _shopDbTransactionManager.RollbackTransaction();
                throw;
            }

            return (basket, errors);
        }

        public async Task<(Basket basket, BusinessErrorCollection errors)> DeleteItemInBasket(string userId, int productId)
        {
            var product = await _productBusinessComponent.GetProductById(productId);
            var errors = new BusinessErrorCollection();

            // Validate product is found
            if (product == null)
            {
                errors.Add(BusinessErrors.P001ProductNotFound);
                return (null, errors);
            }

            // Validate basket is found
            var basket = await _basketRepository.GetBasketByUserId(userId);
            if (basket == null)
            {
                errors.Add(BusinessErrors.B001BasketNotFound);
                return (null, errors);
            }

            // Validate product is present in basket
            var item = basket.Items.SingleOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                errors.Add(BusinessErrors.I001BasketItemNotFound);
                return (null, errors);
            }

            if (!errors.IsValid)
                return (basket, errors);

            try
            {
                await _shopDbTransactionManager.BeginTransaction();

                await _basketRepository.DeleteBasketItem(item);
                await _shopDbTransactionManager.CommitTransaction();
            }
            catch
            {
                await _shopDbTransactionManager.RollbackTransaction();
                throw;
            }

            return (basket, errors);
        }
    }
}
