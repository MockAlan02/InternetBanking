using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Domain.Enums;
using InternetBanking.Infrastructure.Identity.Entities;

using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Seeding
{
    internal class DefaultAdminSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository<BasicUser> _userRepository;

        public DefaultAdminSeeder(UserManager<ApplicationUser> userManager,
                                  IUserRepository<BasicUser> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task SeedAsync()
        {
            ApplicationUser adminUser = new()
            {
                UserName = "admin.marcelo",
                Email = "marcelo.diaz.admin@example.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                IdentificationCard = "002-2345678-9",
            };
            if (await _userManager.FindByEmailAsync(adminUser.Email) is { } user)
            {
                adminUser = user;
            }
            else
            {
                await _userManager.CreateAsync(adminUser, "123Pa$$word!");
                await _userManager.AddToRoleAsync(adminUser, nameof(UserType.Admin));
            }
            BasicUser? basicUser = await _userRepository.GetByIdAsync(adminUser.Id);

            if (basicUser is not null)
            {
                if (basicUser.UserType != UserType.Admin)
                {
                    basicUser.UserType = UserType.Admin;
                    await _userRepository.UpdateAsync(basicUser, basicUser.Id!);
                }
            }
            else
            {
                Console.WriteLine(basicUser);
                await _userRepository.AddAsync(
                    new Admin
                    {
                        Id = adminUser.Id,
                        Name = "Marcelo",
                        LastName = "Diaz",
                        IdentificationCard = adminUser.IdentificationCard,
                        IsActive = true,
                    }
                );
            }
        }
    }
}