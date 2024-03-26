namespace ObjectOrientedDesign.FlightSystem.Object;

public class Flight : FlightSystemObject
{
    public double Angle = 0;

    public Flight(ulong id, ulong originId, ulong targetId, DateTime takeoffTime, DateTime landingTime,
        float longitude,
        float latitude, float? amsl, ulong planeId, ulong[] crew, ulong[] load)
    {
        Id = id;
        OriginId = originId;
        TargetId = targetId;
        TakeoffTime = takeoffTime;
        LandingTime = landingTime;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = amsl;
        PlaneID = planeId;
        Crew = crew;
        Load = load;
    }

    public ulong Id { get; set; }
    public ulong OriginId { get; set; }
    public ulong TargetId { get; set; }
    public DateTime TakeoffTime { get; set; }
    public DateTime LandingTime { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float? AMSL { get; set; }
    public ulong PlaneID { get; set; }
    public ulong[] Crew { get; set; }
    public ulong[] Load { get; set; }
}