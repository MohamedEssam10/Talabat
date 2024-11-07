using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs
{
    public class BasketItemDto
    {
        int Id { get; set; }
        public string Name { get; set; } = null!; 
        public string PictureUrl { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Type { get; set; } = null!;
        [Range(0.1, double.MaxValue, ErrorMessage = "Price cannot be Zero.")]
        public decimal Price { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Quantity must be one item at least.")]
        public int Quantity { get; set; }
    }
}
