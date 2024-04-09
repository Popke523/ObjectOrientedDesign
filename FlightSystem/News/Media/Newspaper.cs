using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.News.Media;

public class Newspaper(string name) : Medium
{
    public string Name { get; init; } = name;

    public override string Report(Airport a)
    {
        return $"{Name} - A report from the {a.Name} airport, {a.Country}.";
    }

    public override string Report(CargoPlane c)
    {
        return $"{Name} - An interview with the crew of {c.Serial}.";
    }

    public override string Report(PassengerPlane p)
    {
        return
            $"{Name} - Breaking news! {p.Model} aircraft loses EASA fails certification after inspection of {p.Serial}.";
    }
}