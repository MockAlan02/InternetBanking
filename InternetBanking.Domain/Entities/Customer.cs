using InternetBanking.Domain.Enums;

namespace InternetBanking.Domain.Entities
{
    public class Customer: BasicUser
    {
        public decimal Amount { get; set; }
        public List<BasicProduct> Products { get; set; } = new List<BasicProduct>();
        public override UserType UserType { get; set; } = UserType.Customer;
    }
}
