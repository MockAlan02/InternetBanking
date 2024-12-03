using AutoMapper;
using InternetBanking.Application.Dto.Login;
using InternetBanking.Application.Dto.Register;
using InternetBanking.Application.Dto.User;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Admin;
using InternetBanking.Application.ViewModels.Customer;
using InternetBanking.Application.ViewModels.User;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IAdminService _adminService;
        private readonly IUserRepository<BasicUser> _userRepository;

        public UserService(IAccountService accountService,
              IMapper mapper,
              ICustomerService customerService,
              IAdminService adminService,
              IUserRepository<BasicUser> userRepository)
        {
            _accountService = accountService;
            _mapper = mapper;
            _customerService = customerService;
            _adminService = adminService;
            _userRepository = userRepository;
        }

        public async Task<Result<RegisterResponse>> RegisterCustomer(CustomerSaveViewModel vm)
        {
            var request = _mapper.Map<RegisterRequest>(vm);
            request.UserType = Enums.UserType.Customer;
            // Alternative
            // return await _accountService.Register(request)
            //                             .Peek(_ => _customerService.Add(vm));
            var response = await _accountService.Register(request);
            if (response.IsSuccess)
            {
                await _customerService.Add(vm);
            }
            return response;
        }

        public async Task<Result<RegisterResponse>> RegisterAdmin(AdminSaveViewModel vm)
        {
            var request = _mapper.Map<RegisterRequest>(vm);
            request.UserType = Enums.UserType.Admin;
            // Alternative
            // return await await _accountService.Register(request)
            //                                   .Peek(_ => _adminService.Add(vm));
            var response = await _accountService.Register(request);
            if (response.IsSuccess)
            {
                await _adminService.Add(vm);
            }
            return response;
        }

        public async Task<Result<LoginResponse>> Login(LoginRequest loginRequest)
        {
            return await _accountService.AuthenticationAsync(loginRequest);
        }

        public async Task<UsersInGroupsViewModel> GetAllUsersInGroup(UserFilterDTO dto)
        {
            UsersInGroupsDTO usersInGroupsDTO = await _userRepository.GetAllUsersInGroup();
            if (dto.FullName is not null)
            {
                usersInGroupsDTO.ActiveAdmins = usersInGroupsDTO.ActiveAdmins
                                                                .Where(FilterUsers)
                                                                .ToList();

                usersInGroupsDTO.ActiveCustomers = usersInGroupsDTO.ActiveCustomers
                                                                   .Where(FilterUsers)
                                                                   .ToList();

                usersInGroupsDTO.UnactiveAdmins = usersInGroupsDTO.UnactiveAdmins
                                                                  .Where(FilterUsers)
                                                                  .ToList();

                usersInGroupsDTO.UnactiveCustomers = usersInGroupsDTO.UnactiveCustomers
                                                                     .Where(FilterUsers)
                                                                     .ToList();
            }
            return _mapper.Map<UsersInGroupsViewModel>(usersInGroupsDTO);

            bool FilterUsers(BasicUser user)
            {
                return (user.Name + " " + user.LastName).ToLower().Contains(dto.FullName);
            }
        }

        public async Task SetIsActiveUser(bool activate, string userId)
        {
            BasicUser? basicUser = await _userRepository.GetByIdAsync(userId);
            if (basicUser is null)
            {
                return;
            }

            basicUser.IsActive = activate;
            await _userRepository.UpdateAsync(basicUser, userId);
        }
    }
}
