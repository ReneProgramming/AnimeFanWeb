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
    }
}
