using InternetBanking.Application.Dto.Login;
using InternetBanking.Application.Dto.Register;
using InternetBanking.Application.Extras.ResultObject;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Result<RegisterResponse>> Register(RegisterRequest request);
        Task<Result<LoginResponse>> AuthenticationAsync(LoginRequest request);
        Task SignOutAsync();
    }
}