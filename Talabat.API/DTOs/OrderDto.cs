namespace Talabat.API.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
        public AddressToReturnDto ShippingAddress { get; set; } = null!;
    }
}
