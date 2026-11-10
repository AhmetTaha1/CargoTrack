using System.ComponentModel.DataAnnotations;

namespace CargoTrack.Application.DTOs;

public class RegisterRequest
{
    [Required(ErrorMessage = "Email zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
    [StringLength(100, ErrorMessage = "Bu alan en fazla 100 karakter olabilir")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifre 8 ile 100 karakter arasında olmalıdır")]
    public string Password { get; set; } = string.Empty;

}