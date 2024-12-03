using InternetBanking.Application.Enums;
using InternetBanking.Application.Interfaces.ViewModels;
using InternetBanking.Domain.Enums;

namespace InternetBanking.Application.ViewModels.Product
{
    public class ProductSaveViewModel
    : IProductSaveViewModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string IdentifierAccount { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public decimal Amount { get; set; }
        public decimal Limit { get; set; }

    }
}
