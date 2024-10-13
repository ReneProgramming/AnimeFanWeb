using Microsoft.AspNetCore.Identity;

namespace AnimeFanWeb.Models
{
#nullable disable
    public static class IdentityHelper
    {
        public const string Moderator = "Moderator";
        public const string Fan = "Fan";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();

            // If no users are present, make the default user
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count();
            if (numUsers == 0) // If no users are in the specified role
            {
                var defaultUser = new IdentityUser()
                {
                    Email = "moderator@animefan.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(defaultUser, "Programming#1");

                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
