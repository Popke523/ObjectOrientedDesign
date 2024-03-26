namespace ObjectOrientedDesign.FlightSystem.Object;

public class PassengerPlane(
    ulong id,
    string serial,
    string country,
    string model,
    ushort firstClassSize,
    ushort businessClassSize,
    ushort economyClassSize)
    : FlightSystemObject
{
    public ulong Id { get; set; } = id;
    public string Serial { get; set; } = serial;
    public string Country { get; set; } = country;
    public string Model { get; set; } = model;
    public ushort FirstClassSize { get; set; } = firstClassSize;
    public ushort BusinessClassSize { get; set; } = businessClassSize;
    public ushort EconomyClassSize { get; set; } = economyClassSize;
}