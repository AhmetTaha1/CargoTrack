namespace CargoTrack.Models;

public class Shipment
{
    public string TrackingNumber { get; set; } = string.Empty;
    public string SenderCity { get; set; } = string.Empty;
    public string ReceiverCity { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

}