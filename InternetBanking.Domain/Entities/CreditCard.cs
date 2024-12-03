using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class CreditCard:BasicProduct
    {
        public decimal Limit { get; set; }
        public decimal LoanAmount { get; set; }
        public override ProductType ProductType { get; set; } = ProductType.CreditCard;
    }
}
