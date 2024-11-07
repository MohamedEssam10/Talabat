using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {}
            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
                builder.Entity<ApplicationUser>().ToTable("Users");
                builder.Entity<IdentityRole<int>>().ToTable("Roles");
                builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
                builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
                builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
                builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
                builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            }
        }
}
