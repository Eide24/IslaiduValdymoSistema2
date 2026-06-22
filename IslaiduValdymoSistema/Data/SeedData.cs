using Microsoft.Extensions.DependencyInjection;
using Google.Apis.Admin.Directory.directory_v1.Data;
using IslaiduValdymoSistema.Models;
using Microsoft.AspNetCore.Identity;

namespace IslaiduValdymoSistema.Data
{
    public static class SeedData
    {
        public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Administratorius", "Klientas" };
            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "admin@test.lt";
            string adminPassword = "Admin123!";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

           
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createResult.Succeeded)
                {
                   
                    return;
                }
            }

          
            if (!await userManager.IsInRoleAsync(adminUser, "Administratorius"))
            {
                await userManager.AddToRoleAsync(adminUser, "Administratorius");
            }
        }
    }
}

