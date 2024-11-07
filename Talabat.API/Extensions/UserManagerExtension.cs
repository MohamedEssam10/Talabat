using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.API.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser?> FindUserWithAddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal UserClaims)
        {
            var Email = UserClaims.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Where(U => U.Email ==  Email).Include(U => U.Address).FirstOrDefaultAsync();
 
            return user;
        }
    }
}
