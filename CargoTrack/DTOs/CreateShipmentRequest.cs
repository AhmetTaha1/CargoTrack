using System.ComponentModel.DataAnnotations;

namespace CargoTrack.DTOs;

public class CreateShipmentRequest
{
    [Required(ErrorMessage = "Lütfen göndericinin şehir bilgisini giriniz")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Bu alan 2 ile 50 karakter arasında olmalıdır.")]
    public string SenderCity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen alıcının şehir bilgisini giriniz")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Bu alan 2 ile 50 karakter arasında olmalıdır.")]
    public string ReceiverCity { get; set; } = string.Empty;


}