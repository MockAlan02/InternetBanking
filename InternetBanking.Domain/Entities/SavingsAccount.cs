using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class SavingsAccount:BasicProduct
    {
        public bool IsMain { get; set; }
        public override ProductType ProductType { get; set; } = ProductType.SavingAccount;
    }
}
