using CargoTrack.DTOs;
using CargoTrack.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrack.Controllers;

[ApiController]
[Route("api/shipments")]
public class ShipmentsController : ControllerBase
{

    private static readonly List<Shipment> _shipments =
    [
        new Shipment {TrackingNumber="21312",SenderCity="Istanbul",ReceiverCity="Ankara",Status=ShipmentStatus.InTransit},
        new Shipment {TrackingNumber="31423",SenderCity="Ankara",ReceiverCity="Izmir",Status=ShipmentStatus.Delivered},
        new Shipment {TrackingNumber="12343",SenderCity="Istanbul",ReceiverCity="Manisa",Status=ShipmentStatus.Received},


    ];


    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_shipments);
    }

    [HttpGet("{trackingNumber}")]
    public IActionResult GetByTrackingNumber(string trackingNumber)
    {
        var shipment = _shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);

        if (shipment is null)
            return NotFound();

        return Ok(shipment);

    }

    [HttpPost]
    public IActionResult Create(CreateShipmentRequest request)
    {
        var shipment = new Shipment
        {
            TrackingNumber = $"TR{Random.Shared.Next(100000, 999999)}",
            SenderCity = request.SenderCity,
            ReceiverCity = request.ReceiverCity,
            Status=ShipmentStatus.Received
        };

        _shipments.Add(shipment);


        return CreatedAtAction(
            nameof(GetByTrackingNumber),
            new { trackingNumber = shipment.TrackingNumber }, shipment
        );
    }

    [HttpPut("{trackingNumber}")]
    public IActionResult Update(string trackingNumber, UpdateShipmentRequest request)
    {
        var shipment = _shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);
        if (shipment is null)
            return NotFound();

        if (shipment.Status == ShipmentStatus.Delivered)
            return Conflict("Teslim edilmiş kargo güncellenemez.");

        shipment.ReceiverCity = request.ReceiverCity;
        shipment.SenderCity = request.SenderCity;
        shipment.Status = request.Status!.Value;

        return NoContent();
    }

    [HttpDelete("{trackingNumber}")]
    public IActionResult Delete(string trackingNumber)
    {
        var shipment = _shipments.FirstOrDefault(s => s.TrackingNumber == trackingNumber);

        if (shipment is null)
            return NotFound();

        if (shipment.Status != ShipmentStatus.Received)
            return Conflict("Sadece 'Alındı' durumundaki kargolar iptal edilebilir");

        _shipments.Remove(shipment);

        return NoContent();
    }




    [HttpGet("search")]
    public IActionResult Search(string city)
    {
        var result = _shipments.Where(s => string.Equals(s.ReceiverCity, city, StringComparison.OrdinalIgnoreCase)).ToList();

        return Ok(result);
    }









}