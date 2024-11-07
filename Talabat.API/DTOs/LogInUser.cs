using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOs
{
    public class LogInUser
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
