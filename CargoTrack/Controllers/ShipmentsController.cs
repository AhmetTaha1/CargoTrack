using CargoTrack.Application.DTOs;
using CargoTrack.Application.Exceptions;
using CargoTrack.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrack.Controllers;

[ApiController]
[Route("api/shipments")]
public class ShipmentsController : ControllerBase
{
    private readonly IShipmentService _shipmentService;
    public ShipmentsController( IShipmentService shipmentService)
    {
        
        _shipmentService = shipmentService;
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_shipmentService.GetAll());
    }

    [HttpGet("{trackingNumber}")]
    public IActionResult GetByTrackingNumber(string trackingNumber)
    {
        var shipment = _shipmentService.GetByTrackingNumber(trackingNumber);

        if (shipment is null)
            return NotFound();

        return Ok(shipment);

    }

    [HttpPost]
    public IActionResult Create(CreateShipmentRequest request)
    {

        var shipment = _shipmentService.Create(request);

        return CreatedAtAction(
            nameof(GetByTrackingNumber),
            new { trackingNumber = shipment.TrackingNumber }, shipment
        );
    }

    [HttpPut("{trackingNumber}")]
    public IActionResult Update(string trackingNumber, UpdateShipmentRequest request)
    {
        try
        {
            _shipmentService.Update(trackingNumber, request);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        
    }

    [HttpDelete("{trackingNumber}")]
    public IActionResult Delete(string trackingNumber)
    {
        try
        {
            _shipmentService.Delete(trackingNumber);
            return NoContent();
        }catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(ConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPatch("{trackingNumber}/status")]
    public IActionResult UpdateStatus(string trackingNumber, UpdateShipmentStatusRequest request)
    {
        try
        {
            _shipmentService.UpdateStatus(trackingNumber,request);
            return NoContent();
        }catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }


    }


    [HttpGet("search")]
    public IActionResult Search(string city)
    {

        return Ok(_shipmentService.Search(city));
    }









}