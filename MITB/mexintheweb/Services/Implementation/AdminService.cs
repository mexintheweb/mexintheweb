using Microsoft.AspNetCore.Identity;

namespace mexintheweb.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private UserManager<IdentityUser> UserManager { get; }
        private RoleManager<IdentityRole> RoleManager { get; }

        public AdminService(UserManager<IdentityUser> um, RoleManager<IdentityRole> rm)
        {
            UserManager = um;
            RoleManager = rm;
        }

        public async Task CreateAdminAccount()
        {
            var adminUser = new IdentityUser
            {
                UserName = "mexadmin",
                Email = "max2109@live.de"
            };

            
            await UserManager.CreateAsync(adminUser);
            await UserManager.AddPasswordAsync(adminUser, "Mex!1987");

            var adminRole = new IdentityRole
            {
                Name = "adminrole"
            };

            await RoleManager.CreateAsync(adminRole);

            await UserManager.AddToRoleAsync(adminUser, "adminrole");
        }
    }
}
