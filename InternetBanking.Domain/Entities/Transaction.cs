using InternetBanking.Domain.Common;
using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class Transaction
    : BaseEntity
    {
        public TransactionType TransactionType { get; set; }
        public DateTime DateTransanction { get; set; } = DateTime.UtcNow;
    }
}
