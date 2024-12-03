using InternetBanking.Application.Enums;

namespace InternetBanking.Application.Dto.Login
{
    public class LoginResponse
    {
        public string Id { get; set; } = string.Empty;
        public UserType UserType { get; set; }
    }
}
