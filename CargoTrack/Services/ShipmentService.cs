using Microsoft.EntityFrameworkCore;
using CargoTrack.Data;
using CargoTrack.DTOs;
using CargoTrack.Entities;

namespace CargoTrack.Services;

public class ShipmentService : IShipmentService
{
    
    private readonly AppDbContext _context;

    public ShipmentService(AppDbContext context)
    {
        _context=context;
    }

    public List<Shipment> GetAll()
    {
        return _context.Shipments.ToList();
    }

    public Shipment? GetByTrackingNumber(string trackingNumber)
    {
        return _context.Shipments.FirstOrDefault(s=>s.TrackingNumber==trackingNumber);
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
        return _context.Shipments.Where(s=>EF.Functions.ILike(s.ReceiverCity, city)).ToList();
    }








}