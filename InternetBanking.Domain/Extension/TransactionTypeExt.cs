using InternetBanking.Domain.Entities;
using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Extension
{
    public static class TransactionTypeExt
    {
        public static Transaction Create(this TransactionType self)
        => new()
        {
            TransactionType = self,
            DateTransanction = DateTime.UtcNow
        };
    }
}