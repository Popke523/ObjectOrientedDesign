namespace ObjectOrientedDesign.FlightSystem.Object;

public class CargoPlane(ulong id, string serial, string country, string model, float maxLoad)
    : FlightSystemObject
{
    public ulong Id { get; set; } = id;
    public string Serial { get; set; } = serial;
    public string Country { get; set; } = country;
    public string Model { get; set; } = model;
    public float MaxLoad { get; set; } = maxLoad;
}