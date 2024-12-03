using InternetBanking.Application.Enums;

namespace InternetBanking.Presentation.DTO
{
    public class LoginInfoDTO
    {
        public required string Id { get; set; }
        public required UserType UserType { get; set; }
    }
}