using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Api.Model
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
