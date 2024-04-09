using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.News.Media;

public abstract class Medium
{
    public abstract string Report(Airport a);
    public abstract string Report(CargoPlane c);
    public abstract string Report(PassengerPlane p);
}