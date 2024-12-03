using InternetBanking.Domain.Common;
using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class BasicProduct
    : BaseEntity, IAuditableEntity
    {
        public string IdentifierAccount { get; set; } = string.Empty;
        public virtual ProductType ProductType { get; set; }
        public decimal Amount { get; set; }
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
