using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.News.Media;

public class Radio(string name) : Medium
{
    public string Name { get; init; } = name;

    public override string Report(Airport a)
    {
        return $"Reporting for {Name}, Ladies and gentlemen, we are at the {a.Name} airport.";
    }

    public override string Report(CargoPlane c)
    {
        return $"Reporting for {Name}, Ladies and gentlemen, we are seeing the {c.Serial} aircraft fly above us.";
    }

    public override string Report(PassengerPlane p)
    {
        return $"Reporting for {Name}, Ladies and gentlemen, we've just witnessed {p.Serial} takeoff.";
    }
}