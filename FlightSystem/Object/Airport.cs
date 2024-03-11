namespace ObjectOrientedDesign.FlightSystem.Object;

public class Airport : FlightSystemObject
{
    public Airport(ulong id, string name, string code, float longitude, float latitude, float amsl, string country)
    {
        Id = id;
        Name = name;
        Code = code;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = amsl;
        Country = country;
    }

    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public string Country { get; set; }
}