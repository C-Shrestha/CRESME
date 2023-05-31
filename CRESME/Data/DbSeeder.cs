using CRESME.Constants;
using Microsoft.AspNetCore.Identity;

namespace CRESME.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Instructor.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));

            // creating admin

            var user = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Admin",
                Role = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            var user1 = new ApplicationUser
            {
                UserName = "user1@gmail.com",
                Email = "user1@gmail.com",
                Name = "Jacob",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Term = "2022-2023",
                Course = "PEDIATRICS",
                Block = "5"

            };
            var user2 = new ApplicationUser
            {
                UserName = "user2@gmail.com",
                Email = "user2@gmail.com",
                Name = "Michael",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Term = "2023-24",
                Course = "PRACTICE OF MEDICINE",
                Block = "6"

            };
            var user3 = new ApplicationUser
            {
                UserName = "user3@gmail.com",
                Email = "user3@gmail.com",
                Name = "Jason",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Term = "2022-2023",
                Course = "PEDIATRICS",
                Block = "5"

            };
            await userManager.CreateAsync(user1, "Password@123");
            await userManager.AddToRoleAsync(user1, Roles.Student.ToString());
            await userManager.CreateAsync(user2, "Password@123");
            await userManager.AddToRoleAsync(user2, Roles.Student.ToString());
            await userManager.CreateAsync(user3, "Password@123");
            await userManager.AddToRoleAsync(user3, Roles.Student.ToString());
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                // the passowrd for the Admin is Admin@123. 
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }

    }
}
