namespace ObjectOrientedDesign.FlightSystem;

public class UnixTimeStamp
{
    public static DateTime ToDateTime(long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp);
        return dateTimeOffset.UtcDateTime;
    }
}