using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class ApplicationIdentityDbContextSeed
    {
        
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Mohamed Essam",
                    Email = "Mo7amed6102003@gmail.com",
                    UserName = "Mohamed_10",
                    PhoneNumber = "0100 250 3550",
                    NormalizedUserName = "MOHAMED_10" 
                };

                var Result = await userManager.CreateAsync(user, "123456");

                if(Result.Succeeded)
                    Console.WriteLine("***************Done***************");
            }
        }
    }
}
