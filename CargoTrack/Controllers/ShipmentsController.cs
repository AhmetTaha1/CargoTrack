using CargoTrack.Data;
using CargoTrack.DTOs;
using CargoTrack.Entities;
using CargoTrack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoTrack.Controllers;

[ApiController]
[Route("api/shipments")]
public class ShipmentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IShipmentService _shipmentService;
    public ShipmentsController(AppDbContext context,IShipmentService shipmentService)
    {
        _context = context;
        _shipmentService=shipmentService;
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_shipmentService.GetAll());
    }

    [HttpGet("{trackingNumber}")]
    public IActionResult GetByTrackingNumber(string trackingNumber)
    {
        var shipment=_shipmentService.GetByTrackingNumber(trackingNumber);

        if (shipment is null)
            return NotFound();

        return Ok(shipment);

    }

    [HttpPost]
    public IActionResult Create(CreateShipmentRequest request)
    {
       
       var shipment=_shipmentService.Create(request);

        return CreatedAtAction(
            nameof(GetByTrackingNumber),
            new { trackingNumber = shipment.TrackingNumber }, shipment
        );
    }

    [HttpPut("{trackingNumber}")]
    public IActionResult Update(string trackingNumber, UpdateShipmentRequest request)
    {
        var shipment = _context.Shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);
        if (shipment is null)
            return NotFound();

        if (shipment.Status == ShipmentStatus.Delivered)
            return Conflict("Teslim edilmiş kargo güncellenemez.");

        shipment.ReceiverCity = request.ReceiverCity;
        shipment.SenderCity = request.SenderCity;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{trackingNumber}")]
    public IActionResult Delete(string trackingNumber)
    {
        var shipment = _context.Shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);

        if (shipment is null)
            return NotFound();

        if (shipment.Status != ShipmentStatus.Received)
            return Conflict("Sadece 'Alındı' durumundaki kargolar iptal edilebilir");

        _context.Shipments.Remove(shipment);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{trackingNumber}/status")]
    public IActionResult UpdateStatus(string trackingNumber, UpdateShipmentStatusRequest request)
    {
        var shipment = _context.Shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);
        if (shipment is null)
            return NotFound();

        var newStatus = request.Status!.Value;

        if (newStatus <= shipment.Status)
            return Conflict($"Durum {shipment.Status} → {newStatus} olarak değiştirilemez. Sadece ileri gidilebilir.");

        shipment.Status = newStatus;
        _context.SaveChanges();
        return NoContent();


    }




    [HttpGet("search")]
    public IActionResult Search(string city)
    {

        return Ok(_shipmentService.Search(city));
    }









}