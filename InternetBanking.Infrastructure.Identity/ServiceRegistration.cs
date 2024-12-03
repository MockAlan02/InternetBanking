using System.Reflection;

using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Context;
using InternetBanking.Infrastructure.Identity.Entities;
using InternetBanking.Infrastructure.Identity.Seeding;
using InternetBanking.Infrastructure.Identity.Service;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddIdentityLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services.AddIdentityLayerDbContext(config)
                           .ConfigureIdentityLayer()
                           .AddTransient<DefaultRolesSeeder>()
                           .AddTransient<DefaultAdminSeeder>()
                           .AddTransient<IAccountService, AccountService>()
                           .AddTransient<DefaultCustomerSeeder>();
        }

        private static IServiceCollection ConfigureIdentityLayer(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
                        options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<InternetBankingIdentityContext>();

            services
            .Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Authentication/Index";
                options.AccessDeniedPath = "/Authentication/AccessDenied";
                options.SlidingExpiration = true;
            });
            return services;
        }

        private static IServiceCollection AddIdentityLayerDbContext(this IServiceCollection services,
                                                                    IConfiguration config)
        {
            string? connectionString = config.GetConnectionString("IdentityConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("IdentityConnection cannot be unset in the configuration.");
            }

            services.AddDbContext<InternetBankingIdentityContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    m => m.MigrationsAssembly(typeof(InternetBankingIdentityContext).Assembly.FullName)
                );
            });

            return services;
        }
    }
}