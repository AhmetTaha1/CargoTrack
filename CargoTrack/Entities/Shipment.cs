namespace CargoTrack.Entities;

public class Shipment
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string SenderCity { get; set; } = string.Empty;
    public string ReceiverCity { get; set; } = string.Empty;
    public ShipmentStatus Status { get; set; }

}