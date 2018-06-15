using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Api.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public decimal BasePrice { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
    }
}
