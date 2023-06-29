using CRESME.Constants;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

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
                UserName = "cresmeAdmin",
                Email = "cresmeAdmin",
                Name = "cresmeAdmin",
                Role = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            
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
