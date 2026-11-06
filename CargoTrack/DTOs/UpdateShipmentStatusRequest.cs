using CargoTrack.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace CargoTrack.DTOs;


public class UpdateShipmentStatusRequest
{   
    [Required(ErrorMessage = "Durum zorunludur.")]
    public ShipmentStatus? Status { get; set; }
}