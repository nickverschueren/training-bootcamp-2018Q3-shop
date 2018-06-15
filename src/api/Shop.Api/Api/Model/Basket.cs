using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Api.Model
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<BasketItem> Items { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime Created { get; set; }    }
}
