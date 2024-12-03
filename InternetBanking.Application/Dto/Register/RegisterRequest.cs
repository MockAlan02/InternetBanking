using InternetBanking.Application.Enums;

namespace InternetBanking.Application.Dto.Register
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IdentificationCard { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType UserType { get; set; }

    }
}
