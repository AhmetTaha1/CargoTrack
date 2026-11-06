using System.ComponentModel.DataAnnotations;
using CargoTrack.Domain.Entities;


namespace CargoTrack.Application.DTOs;

public class UpdateShipmentRequest
{
    [Required(ErrorMessage = "Lütfen göndericinin şehir bilgisini giriniz")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Bu alan 2 ile 50 karakter arasında olmalıdır.")]
    public string SenderCity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lütfen alıcının şehir bilgisini giriniz")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Bu alan 2 ile 50 karakter arasında olmalıdır.")]
    public string ReceiverCity { get; set; } = string.Empty;

   

}