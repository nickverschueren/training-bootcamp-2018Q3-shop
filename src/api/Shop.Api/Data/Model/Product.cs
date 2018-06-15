namespace Shop.Api.Data.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public decimal BasePrice { get; set; }
        public Stock Stock { get; set; }
    }
}
