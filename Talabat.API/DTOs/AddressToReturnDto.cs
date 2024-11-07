using Talabat.Core.Entities.Identity;

namespace Talabat.API.DTOs
{
    public class AddressToReturnDto
    {
        public string Username { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Street { get; set; } = null!;
    }
}
