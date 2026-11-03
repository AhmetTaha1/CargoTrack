using System.ComponentModel.DataAnnotations;

namespace CargoTrack.DTOs;

public class CreateShipmentRequest
{
    [Required(ErrorMessage = "Lütfen göndericinin şehir bilgisini giriniz")]
    [StringLength(50, MinimumLength = 2)]
    public string SenderCity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen alıcının şehir bilgisini giriniz")]
    [StringLength(50, MinimumLength = 2)]
    public string ReceiverCity { get; set; } = string.Empty;


}