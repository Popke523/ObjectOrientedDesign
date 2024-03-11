using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem;

public class FlightSystem
{
    public List<Airport> Airports;
    public List<Cargo> Cargoes;
    public List<CargoPlane> CargoPlanes;
    public List<Crew> Crew;
    public List<Flight> Flights;
    public List<PassengerPlane> PassengerPlanes;
    public List<Passenger> Passengers;

    public FlightSystem(string filename)
    {
    }
}