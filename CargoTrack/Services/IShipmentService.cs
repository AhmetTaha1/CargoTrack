using CargoTrack.Application.DTOs;
using CargoTrack.Domain.Entities;

namespace CargoTrack.Services;

public interface IShipmentService
{
    
    List<Shipment> GetAll();
    Shipment? GetByTrackingNumber(string trackingNumber);
    Shipment Create(CreateShipmentRequest request);
    List<Shipment> Search(string city);

    void Update(string trackingNumber,UpdateShipmentRequest request);
    void UpdateStatus(string trackingNumber,UpdateShipmentStatusRequest request);
    void Delete(string trackingNumber);





}