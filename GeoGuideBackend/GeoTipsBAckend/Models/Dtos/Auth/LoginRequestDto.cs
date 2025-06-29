using System.ComponentModel.DataAnnotations;

namespace GeoTipsBackend.Models.Dtos.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; } = string.Empty;
    }
}