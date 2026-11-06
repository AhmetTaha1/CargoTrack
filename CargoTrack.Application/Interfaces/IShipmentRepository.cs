using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Interfaces;

public interface IShipmentRepository
{
     List<Shipment> GetAll();
    Shipment? GetByTrackingNumber(string trackingNumber);
    
    List<Shipment> Search(string city);
    bool TrackingNumberExists(string trackingNumber);
    void Add(Shipment shipment);
    void Remove(Shipment shipment);
    void SaveChanges();
    






}