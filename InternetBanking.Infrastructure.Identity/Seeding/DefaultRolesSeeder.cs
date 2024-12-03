using InternetBanking.Domain.Enums;

using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Seeding
{
    internal class DefaultRolesSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DefaultRolesSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            HashSet<string?> identityRoles = _roleManager.Roles
                                                        .Select(i => i.Name)
                                                        .ToHashSet();
            foreach (var type in Enum.GetNames<UserType>())
            {
                if (!identityRoles.Contains(type))
                {
                    await _roleManager.CreateAsync(new IdentityRole(type));
                }
            }
        }
    }
}