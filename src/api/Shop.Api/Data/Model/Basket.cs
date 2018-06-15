using System;
using System.Collections.Generic;

namespace Shop.Api.Data.Model
{
    public class Basket : IAuditable
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<BasketItem> Items { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime Created { get; set; }
    }
}
