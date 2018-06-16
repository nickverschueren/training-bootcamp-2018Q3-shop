using System;
using System.Collections.Generic;

namespace Shop.Api.Api.Model
{
    public class Basket
    {
        public string UserId { get; set; }
        public ICollection<BasketItem> Items { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime Created { get; set; }
    }
}
