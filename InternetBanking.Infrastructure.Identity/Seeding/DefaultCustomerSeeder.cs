using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Domain.Enums;
using InternetBanking.Infrastructure.Identity.Entities;

using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Seeding
{
    internal class DefaultCustomerSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository<BasicUser> _userRepository;

        public DefaultCustomerSeeder(UserManager<ApplicationUser> userManager, IUserRepository<BasicUser> userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task SeedAsync()
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "123",
                Email = "carla.santos89@example.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                IdentificationCard = "001-1234567-8",
            };

            if (await _userManager.FindByEmailAsync(defaultUser.Email) is { } user)
            {
                defaultUser = user;
            }
            else
            {
                await _userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await _userManager.AddToRoleAsync(defaultUser, nameof(UserType.Customer));
            }

            BasicUser? basicUser = await _userRepository.GetByIdAsync(defaultUser.Id);
            if (basicUser is not null)
            {
                if (basicUser.UserType != UserType.Customer)
                {
                    basicUser.UserType = UserType.Customer;
                    await _userRepository.UpdateAsync(basicUser, basicUser.Id!);
                }
            }
            else
            {
                await _userRepository.AddAsync(
                    new Customer
                    {
                        Id = defaultUser.Id,
                        Name = "Carla",
                        LastName = "Santos",
                        IdentificationCard = defaultUser.IdentificationCard,
                        IsActive = true,
                        Amount = 0
                    }
                );
            }
        }
    }
}