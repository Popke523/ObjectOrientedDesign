namespace ObjectOrientedDesign
{
    public class Flight
    {
        ulong ID;
        ulong OriginID;
        ulong TargetID;
        string TakeoffTime;
        string LandingTime;
        float Longitude;
        float Latitude;
        float AMSL;
        ulong PlaneID;
        ulong[] Crew;
        ulong[] Load;
    }
}
