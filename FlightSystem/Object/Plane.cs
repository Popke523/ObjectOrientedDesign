namespace ObjectOrientedDesign.FlightSystem.Object;

public class Plane(ulong id, string serial, string country, string model)
    : FlightSystemObject(id)
{
    public string Serial { get; set; } = serial;
    public string Country { get; set; } = country;
    public string Model { get; set; } = model;
}