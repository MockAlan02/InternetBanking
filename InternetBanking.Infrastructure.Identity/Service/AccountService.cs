using AutoMapper;

using InternetBanking.Application.Dto.Login;
using InternetBanking.Application.Dto.Register;
using InternetBanking.Application.Enums;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Infrastructure.Identity.Entities;

using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager,
              IMapper mapper,
              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<Result<RegisterResponse>> Register(RegisterRequest request)
        {
            // RegisterResponse response = new();
            var usernameExist = await _userManager.FindByNameAsync(_userManager.NormalizeName(request.Username));

            if (usernameExist != null)
            {
                // response.HasError = true;
                // response.Error = $"Username {request.Username} has taken";
                // response.ErrorType = Application.Enums.ErrorType.Username;
                // return response;
                return ErrorType.Username.Because($"Username {request.Username} has been taken");
            }

            ApplicationUser user = _mapper.Map<ApplicationUser>(request);
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;

            var result = await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, request.UserType.ToString());
            if (!result.Succeeded)
            {
                var identityErrors = result.Errors.Where(e => e.Code.StartsWith("Password"));
                if (!identityErrors.Any())
                {
                    // response.HasError = true;
                    // response.Error = result.Errors.FirstOrDefault()!.Description;
                    // response.ErrorType = Application.Enums.ErrorType.Password;
                    // return response;
                    return Result.Fail<RegisterResponse>(identityErrors.Select(e => ErrorType.Password.Because(e.Description)));
                }
                // response.HasError = true;
                // response.Error = "An error ocurred trying to register the user.";
                return ErrorType.Any.Because("An error ocurred trying to register the user.");
            }

            return new RegisterResponse();
        }

        public async Task<Result<LoginResponse>> AuthenticationAsync(LoginRequest request)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(_userManager.NormalizeName(request.Username));
            if (user == null)
            {
                // response.HasError = true;
                // response.Error = "Username doesn't exist";
                // response.ErrorType = Application.Enums.ErrorType.Username;
                // return response;
                return ErrorType.Username.Because("Username doesn't exist");
            }

            var result = await _signInManager.PasswordSignInAsync(user,
                request.Password,
                false,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                // response.HasError = true;
                // response.Error = "Credentials its not valid";
                // response.ErrorType = Application.Enums.ErrorType.Password;
                // return response;
                return ErrorType.Password.Because("Credentials are not valid");
            }
            if (!user.IsActive)
            {
                return ErrorType.Any.Because("Su cuenta esta desactivada, contacte a uno de los administradores.");
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            UserType mainRole;
            if (!Enum.TryParse(roles[0], true, out mainRole))
            {
                throw new InvalidDataException($"Role doesn't exist in application: {roles[0]}");
            }

            return new LoginResponse()
            {
                Id = user.Id,
                UserType = mainRole
            };
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}
