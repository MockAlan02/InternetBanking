using InternetBanking.Application.Interfaces.ViewModels;
using InternetBanking.Domain.Enums;

namespace InternetBanking.Application.ViewModels.CreditCard
{
    public class CreditCardSaveViewModel
    : IProductSaveViewModel
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }
        public string IdentifierAccount { get; set; } = string.Empty;
        public virtual ProductType ProductType { get; set; }
        public decimal Amount { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal Limit { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
