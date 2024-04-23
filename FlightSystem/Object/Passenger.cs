namespace ObjectOrientedDesign.FlightSystem.Object;

public class Passenger(ulong id, string name, ulong age, string phone, string email, string @class, ulong miles)
    : Person(id, name, age, phone, email), ILoadable
{
    public string Class { get; set; } = @class;
    public ulong Miles { get; set; } = miles;
}