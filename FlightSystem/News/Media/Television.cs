using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.News.Media;

public class Television(string name) : Medium
{
    public string Name { get; init; } = name;

    public override string Report(Airport a)
    {
        return $"<An image of {a.Name} airport>";
    }

    public override string Report(CargoPlane c)
    {
        return $"<An image of {c.Serial} cargo plane>";
    }

    public override string Report(PassengerPlane p)
    {
        return $"<An image of {p.Serial} cargo plane>";
    }
}