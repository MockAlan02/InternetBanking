using InternetBanking.Domain.Enums;

namespace InternetBanking.Application.ViewModels.SavingsAccount
{
    public class SavingsAccountViewModel
    {
        public int Id { get; set; }
        public string IdentifierAccount { get; set; } = string.Empty;
        public virtual ProductType ProductType { get; set; }
        public decimal Amount { get; set; }
        public bool IsMain { get; set; }
    }
}
