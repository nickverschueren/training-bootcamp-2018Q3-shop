using System.Collections.Generic;

namespace Shop.Api.Api.Model
{
    public class ProductCollection
    {
        public List<Product> Items { get; set; }
        public int Total { get; set; }
    }
}
