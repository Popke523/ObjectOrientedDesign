namespace ObjectOrientedDesign.FlightSystem.Object;

public class Flight : FlightSystemObject
{
    public double Angle = 0;

    public Flight(ulong id, Airport origin, Airport target, DateTime takeoffTime, DateTime landingTime,
        float longitude,
        float latitude, float? amsl, Plane plane, Crew[] crew, ILoadable[] load) : base(id)
    {
        Origin = origin;
        Target = target;
        TakeoffTime = takeoffTime;
        LandingTime = landingTime;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = amsl;
        Plane = plane;
        Crew = crew;
        Load = load;
    }

    public Airport Origin { get; set; }
    public Airport Target { get; set; }
    public DateTime TakeoffTime { get; set; }
    public DateTime LandingTime { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float? AMSL { get; set; }
    public Plane Plane { get; set; }
    public Crew[] Crew { get; set; }
    public ILoadable[] Load { get; set; }

}