using CargoTrack.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace CargoTrack.Application.DTOs;


public class UpdateShipmentStatusRequest
{   
    [Required(ErrorMessage = "Durum zorunludur.")]
    public ShipmentStatus? Status { get; set; }
}