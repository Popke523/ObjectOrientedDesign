namespace ObjectOrientedDesign.FlightSystem;

public class UnixTimeStamp
{
    // https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
    public static DateTime ToDateTime(long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp);
        return dateTimeOffset.UtcDateTime;
    }
}