namespace ObjectOrientedDesign.FlightSystem.Object;

public class Flight : FlightSystemObject
{
    public Flight(ulong id, ulong originID, ulong targetID, string takeoffTime, string landingTime, float longitude,
        float latitude, float amsl, ulong planeID, ulong[] crew, ulong[] load)
    {
        ID = id;
        OriginID = originID;
        TargetID = targetID;
        TakeoffTime = takeoffTime;
        LandingTime = landingTime;
        Longitude = longitude;
        Latitude = latitude;
        AMSL = amsl;
        PlaneID = planeID;
        Crew = crew;
        Load = load;
    }

    public ulong ID { get; set; }
    public ulong OriginID { get; set; }
    public ulong TargetID { get; set; }
    public string TakeoffTime { get; set; }
    public string LandingTime { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public ulong PlaneID { get; set; }
    public ulong[] Crew { get; set; }
    public ulong[] Load { get; set; }
}