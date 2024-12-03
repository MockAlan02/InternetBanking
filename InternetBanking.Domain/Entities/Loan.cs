using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class Loan:BasicProduct
    {
        public override ProductType ProductType { get; set; } = ProductType.Loan;
    }
}
