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
    public ShipmentsController(IShipmentService shipmentService)
    {

        _shipmentService = shipmentService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _shipmentService.GetAllAsync());
    }

    [HttpGet("{trackingNumber}")]
    public async Task<IActionResult> GetByTrackingNumber(string trackingNumber)
    {
        var shipment = await _shipmentService.GetByTrackingNumberAsync(trackingNumber);

        if (shipment is null)
            return NotFound();

        return Ok(shipment);

    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateShipmentRequest request)
    {

        var shipment = await _shipmentService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetByTrackingNumber),
            new { trackingNumber = shipment.TrackingNumber }, shipment
        );
    }

    [HttpPut("{trackingNumber}")]
    public async Task<IActionResult> Update(string trackingNumber, UpdateShipmentRequest request)
    {
        try
        {
            await _shipmentService.UpdateAsync(trackingNumber, request);
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
    public async Task<IActionResult> Delete(string trackingNumber)
    {
        try
        {
            await _shipmentService.DeleteAsync(trackingNumber);
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

    [HttpPatch("{trackingNumber}/status")]
    public async Task<IActionResult> UpdateStatus(string trackingNumber, UpdateShipmentStatusRequest request)
    {
        try
        {
            await _shipmentService.UpdateStatusAsync(trackingNumber, request);
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


    [HttpGet("search")]
    public async Task<IActionResult> Search(string city)
    {

        return Ok(await _shipmentService.SearchAsync(city));
    }









}