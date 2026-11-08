using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Interfaces;

public interface IShipmentRepository
{
     Task<List<Shipment>> GetAllAsync();
    Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber);
    
    Task<List<Shipment>> SearchAsync(string city);
    Task<bool> TrackingNumberExistsAsync(string trackingNumber);
    void Add(Shipment shipment);
    void Remove(Shipment shipment);
    Task SaveChangesAsync();
    






}