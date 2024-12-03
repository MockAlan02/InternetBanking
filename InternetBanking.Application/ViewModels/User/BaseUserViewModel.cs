using InternetBanking.Application.Enums;

namespace InternetBanking.Application.ViewModels.User
{
    public class BaseUserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentificationCard { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}