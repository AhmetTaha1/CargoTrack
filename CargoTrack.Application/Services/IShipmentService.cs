using CargoTrack.Application.DTOs;
using CargoTrack.Domain.Entities;

namespace CargoTrack.Application.Services;

public interface IShipmentService
{
    
    Task<List<Shipment>> GetAllAsync();
    Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber);
    Task<Shipment> CreateAsync(CreateShipmentRequest request);
    Task<List<Shipment>> SearchAsync(string city);

    Task UpdateAsync(string trackingNumber,UpdateShipmentRequest request);
    Task UpdateStatusAsync(string trackingNumber,UpdateShipmentStatusRequest request);
    Task DeleteAsync(string trackingNumber);





}