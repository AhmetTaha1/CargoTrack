
using CargoTrack.Application.DTOs;
using CargoTrack.Domain.Entities;
using CargoTrack.Application.Exceptions;
using CargoTrack.Application.Interfaces;


namespace CargoTrack.Services;

public class ShipmentService : IShipmentService
{

    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public List<Shipment> GetAll()
    {
       return _repository.GetAll();
    }

    public Shipment? GetByTrackingNumber(string trackingNumber)
    {
       return _repository.GetByTrackingNumber(trackingNumber);
    }

    public Shipment Create(CreateShipmentRequest request)
    {
        string trackingNumber;
        do
        {
            trackingNumber = $"TR{Random.Shared.Next(100000, 999999)}";
        } while (_repository.TrackingNumberExists(trackingNumber));

        var shipment = new Shipment
        {
            TrackingNumber = trackingNumber,
            SenderCity = request.SenderCity,
            ReceiverCity = request.ReceiverCity,
            Status = ShipmentStatus.Received
        };

        _repository.Add(shipment);
        _repository.SaveChanges();


        return shipment;

    }

    public List<Shipment> Search(string city)
    {
        return _repository.Search(city);
    }

    public void Update(string trackingNumber, UpdateShipmentRequest request)
    {

        var shipment = _repository.GetByTrackingNumber(trackingNumber);

        if (shipment is null)
            throw new NotFoundException("Kargo bulunamadı");

        if (shipment.Status == ShipmentStatus.Delivered)
            throw new ConflictException("Teslim edilmiş kargo güncellenemez");

        shipment.ReceiverCity = request.ReceiverCity;
        shipment.SenderCity = request.SenderCity;
        _repository.SaveChanges();

    }

    public void UpdateStatus(string trackingNumber, UpdateShipmentStatusRequest request)
    {

        var shipment = _repository.GetByTrackingNumber(trackingNumber);

        if (shipment is null)
            throw new NotFoundException("Kargo bulunamadı");


        var newStatus = request.Status!.Value;

        if (newStatus <= shipment.Status)
            throw new ConflictException($"Durum {shipment.Status} → {newStatus} olarak değiştirilemez. Sadece ileri gidilebilir.");
        shipment.Status = newStatus;
        _repository.SaveChanges();

    }

    public void Delete(string trackingNumber)
    {
        var shipment=_repository.GetByTrackingNumber(trackingNumber);

        if(shipment is null)
            throw new NotFoundException("Kargo bulunamadı");

        if(shipment.Status!=ShipmentStatus.Received)
            throw new ConflictException("Sadece 'Alındı' durumundaki kargolar iptal edilebilir");
        
        _repository.Remove(shipment);
        _repository.SaveChanges();

        
    }








}