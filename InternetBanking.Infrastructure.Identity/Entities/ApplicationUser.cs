using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class ApplicationUser
    : IdentityUser
    {
        public bool IsActive { get; set; } = true;

        public string IdentificationCard { get; set; } = string.Empty;
    }
}