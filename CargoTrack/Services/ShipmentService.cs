using Microsoft.EntityFrameworkCore;
using CargoTrack.Data;
using CargoTrack.Application.DTOs;
using CargoTrack.Domain.Entities;
using CargoTrack.Application.Exceptions;

namespace CargoTrack.Services;

public class ShipmentService : IShipmentService
{

    private readonly AppDbContext _context;

    public ShipmentService(AppDbContext context)
    {
        _context = context;
    }

    public List<Shipment> GetAll()
    {
        return _context.Shipments.ToList();
    }

    public Shipment? GetByTrackingNumber(string trackingNumber)
    {
        return _context.Shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);
    }

    public Shipment Create(CreateShipmentRequest request)
    {
        string trackingNumber;
        do
        {
            trackingNumber = $"TR{Random.Shared.Next(100000, 999999)}";
        } while (_context.Shipments.Any(s => s.TrackingNumber == trackingNumber));

        var shipment = new Shipment
        {
            TrackingNumber = trackingNumber,
            SenderCity = request.SenderCity,
            ReceiverCity = request.ReceiverCity,
            Status = ShipmentStatus.Received
        };

        _context.Shipments.Add(shipment);
        _context.SaveChanges();


        return shipment;

    }

    public List<Shipment> Search(string city)
    {
        return _context.Shipments.Where(s => EF.Functions.ILike(s.ReceiverCity, city)).ToList();
    }

    public void Update(string trackingNumber, UpdateShipmentRequest request)
    {

        var shipment = _context.Shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);

        if (shipment is null)
            throw new NotFoundException("Kargo bulunamadı");

        if (shipment.Status == ShipmentStatus.Delivered)
            throw new ConflictException("Teslim edilmiş kargo güncellenemez");

        shipment.ReceiverCity = request.ReceiverCity;
        shipment.SenderCity = request.SenderCity;
        _context.SaveChanges();

    }

    public void UpdateStatus(string trackingNumber, UpdateShipmentStatusRequest request)
    {

        var shipment = _context.Shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);

        if (shipment is null)
            throw new NotFoundException("Kargo bulunamadı");


        var newStatus = request.Status!.Value;

        if (newStatus <= shipment.Status)
            throw new ConflictException($"Durum {shipment.Status} → {newStatus} olarak değiştirilemez. Sadece ileri gidilebilir.");
        shipment.Status = newStatus;
        _context.SaveChanges();

    }

    public void Delete(string trackingNumber)
    {
        var shipment=_context.Shipments.FirstOrDefault(s=>s.TrackingNumber==trackingNumber);

        if(shipment is null)
            throw new NotFoundException("Kargo bulunamadı");

        if(shipment.Status!=ShipmentStatus.Received)
            throw new ConflictException("Sadece 'Alındı' durumundaki kargolar iptal edilebilir");
        
        _context.Shipments.Remove(shipment);
        _context.SaveChanges();

        
    }








}