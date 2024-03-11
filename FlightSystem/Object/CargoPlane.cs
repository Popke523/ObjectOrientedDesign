namespace ObjectOrientedDesign.FlightSystem.Object;

public class CargoPlane : FlightSystemObject
{
    public CargoPlane(ulong id, string serial, string country, string model, float maxLoad)
    {
        ID = id;
        Serial = serial;
        Country = country;
        Model = model;
        MaxLoad = maxLoad;
    }

    public ulong ID { get; set; }
    public string Serial { get; set; }
    public string Country { get; set; }
    public string Model { get; set; }
    public float MaxLoad { get; set; }
}