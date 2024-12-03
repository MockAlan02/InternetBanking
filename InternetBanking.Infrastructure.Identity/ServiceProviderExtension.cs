using InternetBanking.Infrastructure.Identity.Seeding;

using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Identity
{
    public static class ServiceProviderExtension
    {
        public static async Task<IServiceProvider> SeedIdentity(this IServiceProvider self)
        {
            await self.GetRequiredService<DefaultRolesSeeder>().SeedAsync();
            await self.GetRequiredService<DefaultAdminSeeder>().SeedAsync();
            await self.GetRequiredService<DefaultCustomerSeeder>().SeedAsync();
            return self;
        }

    }
}