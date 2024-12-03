using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Application.ViewModels.Payment
{
    public class ExpressPaymentSaveViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un numero de cuenta valido")]
        public string IdentifierAccount { get; set; } = string.Empty;
        [Required(ErrorMessage = "Debe Ingresar un monto valido")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Selecciona una cuenta")]

        public string AccountPaymentFrom { get; set; } = string.Empty;
    }
}
