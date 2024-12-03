using AutoMapper;

using InternetBanking.Application.Dto.Login;
using InternetBanking.Application.Dto.Register;
using InternetBanking.Application.Enums;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Admin;
using InternetBanking.Application.ViewModels.Customer;
using InternetBanking.Application.ViewModels.User;
using InternetBanking.Presentation.DTO;
using InternetBanking.Presentation.Interfacces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InternetBanking.Presentation.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILoginStore _loginStore;

        public AuthenticationController(IUserService userService,
                                        IMapper mapper,
                                        IAccountService accountService,
                                        ILoginStore loginStore)
        {
            _userService = userService;
            _mapper = mapper;
            _accountService = accountService;
            _loginStore = loginStore;
        }

        public IActionResult Index(string? returnUrl)
        {
            return View(
                new LoginViewModel()
                {
                    ReturnUrl = returnUrl
                }
            );
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _accountService.SignOutAsync();
            await _loginStore.Clear();
            return LocalRedirect("~/Authentication/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("index", vm);
            }
            LoginRequest login = _mapper.Map<LoginRequest>(vm);
            Result<LoginResponse> result = await _userService.Login(login);
            if (result.IsSuccess)
            {
                if (vm.ReturnUrl is not null)
                {
                    return LocalRedirect(vm.ReturnUrl);
                }
                await result.Map(_mapper.Map<LoginInfoDTO>)
                            .Peek(_loginStore.Put);

                return ChooseRole(result.Value.UserType);
            }

            foreach (AppError error in result.Errors)
            {
                switch (error.Type)
                {
                    case ErrorType.Any:
                        ModelState.AddModelError(string.Empty, error.Message);
                        break;
                    case ErrorType.Username:
                        ModelState.AddModelError(nameof(LoginViewModel.Username), error.Message);
                        break;
                    case ErrorType.Password:
                        ModelState.AddModelError(nameof(LoginViewModel.Password), error.Message);
                        break;
                }
            }
            return View("index", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", vm);
            }

            Result<RegisterResponse> result = await (vm.UserType switch
            {
                UserType.Admin => _userService.RegisterAdmin(_mapper.Map<AdminSaveViewModel>(vm)),
                UserType.Customer => _userService.RegisterCustomer(_mapper.Map<CustomerSaveViewModel>(vm)),
                _ => throw new NotImplementedException()
            });
            // switch (vm.UserType)
            // {
            //     case UserType.Admin:
            //         var admin = _mapper.Map<AdminSaveViewModel>(vm);
            //         result = await _userService.RegisterAdmin(admin);
            //         break;
            //     case UserType.Customer:
            //         var customer = _mapper.Map<CustomerSaveViewModel>(vm);
            //         result = await _userService.RegisterCustomer(customer);
            //         break;
            // }

            if (result.IsSuccess)
            {
                return LocalRedirect("~/Authentication/Index");
            }

            foreach (AppError error in result.Errors)
            {
                switch (error.Type)
                {
                    case ErrorType.Any:
                        ModelState.AddModelError(string.Empty, error.Message);
                        break;
                    case ErrorType.Username:
                        ModelState.AddModelError(nameof(LoginViewModel.Username), error.Message);
                        break;
                    case ErrorType.Password:
                        ModelState.AddModelError(nameof(LoginViewModel.Password), error.Message);
                        break;
                }
            }

            return View("Register", vm);
        }

        [ActionName("ChooseRole")]
        public async Task<IActionResult> ChooseRoleRedirect()
        {
            UserType userType = await _loginStore.Gets(l => l.UserType);
            return ChooseRole(userType);
        }

        private IActionResult ChooseRole(UserType userType)
        => userType switch
        {
            UserType.Admin => LocalRedirect("~/Admin/Index"),
            UserType.Customer => LocalRedirect("~/Customer/Index"),
            _ => throw new NotImplementedException(),
        };
    }
}
