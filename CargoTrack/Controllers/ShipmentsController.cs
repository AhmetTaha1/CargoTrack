using CargoTrack.Models;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrack.Controllers;

[ApiController]
[Route("api/shipments")]
public class ShipmentsController : ControllerBase
{

    private static readonly List<Shipment> _shipments =
    [
        new Shipment {TrackingNumber="21312",SenderCity="Istanbul",ReceiverCity="Ankara",Status="Yolda"},
        new Shipment {TrackingNumber="31423",SenderCity="Ankara",ReceiverCity="Izmir",Status="Teslim Edildi"},
        new Shipment {TrackingNumber="12343",SenderCity="Istanbul",ReceiverCity="Manisa",Status="Hazirlaniyor"},


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

    [HttpGet("search")]
    public IActionResult Search(string city)
    {
        var result = _shipments.Where(s => string.Equals(s.ReceiverCity, city, StringComparison.OrdinalIgnoreCase)).ToList();

        return Ok(result);
    }









}