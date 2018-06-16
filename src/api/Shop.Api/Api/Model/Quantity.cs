using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Api.Model
{
    public class AddQuantity
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }
    }

    public class UpdateQuantity
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}