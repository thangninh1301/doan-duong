using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Helper.Seeding
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(ModelBuilder modelBuilder)
        {
            const string ADMIN_ID = "b74ddd14-6340-4840-95c2-db12554843e5";
            // any guid, but nothing is against to use the same one
            modelBuilder.Entity<IdentityRole>().HasData(
               new() { Id = "01", Name = "Doctor Test", NormalizedName = "Doctor Test" },
               new() { Id = "02", Name = "Admin", NormalizedName = "Admin" },
               new() { Id = "03", Name = "Patient", NormalizedName = "Patient" },
               new() { Id = "04", Name = "Doctor Examines", NormalizedName = "Doctor Examines" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
            };
            user.LockoutEnabled = true;
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "02",
                UserId = ADMIN_ID
            });
        }
    }
}
