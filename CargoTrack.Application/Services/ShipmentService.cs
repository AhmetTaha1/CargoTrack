
using CargoTrack.Application.DTOs;
using CargoTrack.Domain.Entities;
using CargoTrack.Application.Exceptions;
using CargoTrack.Application.Interfaces;


namespace CargoTrack.Application.Services;

public class ShipmentService : IShipmentService
{

    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Shipment>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber)
    {
        return await _repository.GetByTrackingNumberAsync(trackingNumber);
    }

    public async Task<Shipment> CreateAsync(CreateShipmentRequest request)
    {
        string trackingNumber;
        do
        {
            trackingNumber = $"TR{Random.Shared.Next(100000, 999999)}";
        } while (await _repository.TrackingNumberExistsAsync(trackingNumber));

        var shipment = new Shipment
        {
            TrackingNumber = trackingNumber,
            SenderCity = request.SenderCity,
            ReceiverCity = request.ReceiverCity,
            Status = ShipmentStatus.Received
        };

        _repository.Add(shipment);
        await _repository.SaveChangesAsync();


        return shipment;

    }

    public async Task<List<Shipment>> SearchAsync(string city)
    {
        return await _repository.SearchAsync(city);
    }

    public async Task UpdateAsync(string trackingNumber, UpdateShipmentRequest request)
    {

        var shipment =await _repository.GetByTrackingNumberAsync(trackingNumber);

        if (shipment is null)
            throw new NotFoundException("Kargo bulunamadı");

        if (shipment.Status == ShipmentStatus.Delivered)
            throw new ConflictException("Teslim edilmiş kargo güncellenemez");

        shipment.ReceiverCity = request.ReceiverCity;
        shipment.SenderCity = request.SenderCity;
        await _repository.SaveChangesAsync();

    }

    public async Task UpdateStatusAsync(string trackingNumber, UpdateShipmentStatusRequest request)
    {

        var shipment = await _repository.GetByTrackingNumberAsync(trackingNumber);

        if (shipment is null)
            throw new NotFoundException("Kargo bulunamadı");


        var newStatus = request.Status!.Value;

        if (newStatus <= shipment.Status)
            throw new ConflictException($"Durum {shipment.Status} → {newStatus} olarak değiştirilemez. Sadece ileri gidilebilir.");
        shipment.Status = newStatus;
        await _repository.SaveChangesAsync();

    }

    public async Task DeleteAsync(string trackingNumber)
    {
        var shipment = await _repository.GetByTrackingNumberAsync(trackingNumber);

        if (shipment is null)
            throw new NotFoundException("Kargo bulunamadı");

        if (shipment.Status != ShipmentStatus.Received)
            throw new ConflictException("Sadece 'Alındı' durumundaki kargolar iptal edilebilir");

        _repository.Remove(shipment);
        await _repository.SaveChangesAsync();


    }








}