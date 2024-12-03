using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese un Username valido")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese una contraseña valida")]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}
