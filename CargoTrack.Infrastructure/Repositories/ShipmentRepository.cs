using Microsoft.EntityFrameworkCore;
using CargoTrack.Application.Interfaces;
using CargoTrack.Domain.Entities;
using CargoTrack.Infrastructure.Data;

namespace CargoTrack.Infrastructure.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly AppDbContext _context;

    public ShipmentRepository(AppDbContext context)
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
    public List<Shipment> Search(string city)
    {
        return _context.Shipments.Where(s=>EF.Functions.ILike(s.ReceiverCity,city)).ToList();
    }

    public bool TrackingNumberExists(string trackingNumber)
    {
        return _context.Shipments.Any(s=>s.TrackingNumber==trackingNumber);
    }

    public void Add(Shipment shipment)
    {
        _context.Shipments.Add(shipment);
    }

    public void Remove(Shipment shipment)
    {
        _context.Shipments.Remove(shipment);
    }
    public void SaveChanges()
    {
        _context.SaveChanges();
    }







}