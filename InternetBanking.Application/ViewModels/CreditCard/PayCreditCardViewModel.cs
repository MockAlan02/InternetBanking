using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Application.ViewModels.CreditCard
{
    public class PayCreditCardViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? CreditCardNumberAccount { get; set; }
        [Required]
        public string? SavingAccountNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
