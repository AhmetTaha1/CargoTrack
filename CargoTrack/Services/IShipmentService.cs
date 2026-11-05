using CargoTrack.DTOs;
using CargoTrack.Entities;

namespace CargoTrack.Services;

public interface IShipmentService
{
    
    List<Shipment> GetAll();
    Shipment? GetByTrackingNumber(string trackingNumber);
    Shipment Create(CreateShipmentRequest request);
    List<Shipment> Search(string city);





}