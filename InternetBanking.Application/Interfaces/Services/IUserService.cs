using InternetBanking.Application.Dto.Login;
using InternetBanking.Application.Dto.Register;
using InternetBanking.Application.Dto.User;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.ViewModels.Admin;
using InternetBanking.Application.ViewModels.Customer;
using InternetBanking.Application.ViewModels.User;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result<LoginResponse>> Login(LoginRequest loginRequest);
        Task<Result<RegisterResponse>> RegisterAdmin(AdminSaveViewModel vm);
        Task<Result<RegisterResponse>> RegisterCustomer(CustomerSaveViewModel vm);
        Task<UsersInGroupsViewModel> GetAllUsersInGroup(UserFilterDTO dto);
        Task SetIsActiveUser(bool activate, string userId);
    }
}