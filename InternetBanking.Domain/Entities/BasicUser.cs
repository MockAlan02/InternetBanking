using InternetBanking.Domain.Common;
using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class BasicUser
    : BaseEntity<string>, IAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentificationCard { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public virtual UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
