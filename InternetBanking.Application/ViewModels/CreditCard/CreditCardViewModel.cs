using InternetBanking.Domain.Enums;

namespace InternetBanking.Application.ViewModels.CreditCard
{
    public class CreditCardViewModel
    {
        public int Id { get; set; }
        public string IdentifierAccount { get; set; } = string.Empty;
        public virtual ProductType ProductType { get; set; }
        public decimal Amount { get; set; }
        public decimal LoanAmount { get; set; }
    }
}
