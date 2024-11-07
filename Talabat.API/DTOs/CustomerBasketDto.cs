namespace Talabat.API.DTOs
{
    public class CustomerBasketDto
    {
        public string Id { get; set; } = null!;
        public List<BasketItemDto> ItemsDto { get; set; } = null!;

    }
}
