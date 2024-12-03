using System.ComponentModel.DataAnnotations;

using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.ViewModels.Customer
{
    public class CustomerSaveViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre valido")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingrese un apellido valido")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese un Username valido")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingrese un correo electornico valido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingrese un numero de identificacion valido")]
        [RegularExpression(@"^[0-9]{3}-?[0-9]{7}-?[0-9]{1}$",
        ErrorMessage = "El formato del número de identificación no es válido")]
        public string IdentificationCard { get; set; } = string.Empty;
        [Required(ErrorMessage = "Ingrese una contraseña valida")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public List<BasicProduct> Products { get; set; } = new();
        public decimal Amount { get; set; }
    }
}
