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

    public async Task<List<Shipment>> GetAllAsync()
    {
        return await _context.Shipments.ToListAsync();
    }
    public async Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber)
    {
        return await _context.Shipments.FirstOrDefaultAsync(s=>s.TrackingNumber==trackingNumber);
    }
    public async Task<List<Shipment>> SearchAsync(string city)
    {
        return await _context.Shipments.Where(s=>EF.Functions.ILike(s.ReceiverCity,city)).ToListAsync();
    }

    public async Task<bool> TrackingNumberExistsAsync(string trackingNumber)
    {
        return await _context.Shipments.AnyAsync(s=>s.TrackingNumber==trackingNumber);
    }

    public void Add(Shipment shipment)
    {
        _context.Shipments.Add(shipment);
    }

    public void Remove(Shipment shipment)
    {
        _context.Shipments.Remove(shipment);
    }
    public async Task SaveChangesAsync()
    {
       await _context.SaveChangesAsync();
    }







}