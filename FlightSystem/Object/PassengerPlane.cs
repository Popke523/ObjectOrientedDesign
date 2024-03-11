namespace ObjectOrientedDesign.FlightSystem.Object;

public class PassengerPlane : FlightSystemObject
{
    public PassengerPlane(ulong id, string serial, string country, string model, ushort firstClassSize,
        ushort businessClassSize, ushort economyClassSize)
    {
        ID = id;
        Serial = serial;
        Country = country;
        Model = model;
        FirstClassSize = firstClassSize;
        BusinessClassSize = businessClassSize;
        EconomyClassSize = economyClassSize;
    }

    public ulong ID { get; set; }
    public string Serial { get; set; }
    public string Country { get; set; }
    public string Model { get; set; }
    public ushort FirstClassSize { get; set; }
    public ushort BusinessClassSize { get; set; }
    public ushort EconomyClassSize { get; set; }
}