using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Api.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal BasePrice { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
    }

    public class NewProduct
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Desc { get; set; }
        [MaxLength(500)]
        public string Image { get; set; }
        [Range(0, int.MaxValue)]
        public decimal? Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal? BasePrice { get; set; }
        [Range(0, int.MaxValue)]
        public int? Stock { get; set; }
    }

    public class UpdateProduct : NewProduct
    {
    }
}
