using ObjectOrientedDesign.FlightSystem.Factory;

namespace ObjectOrientedDesign.FlightSystem.Reader;

public class FtrDataReader(StreamReader streamReader)
{
    private StreamReader StreamReader { get; } = streamReader;

    public FlightSystem ToFlightSystem()
    {
        var flightSystem = new FlightSystem();
        AddToFlightSystem(flightSystem);
        return flightSystem;
    }

    public void AddToFlightSystem(FlightSystem flightSystem)
    {
        string? line;
        while ((line = StreamReader.ReadLine()) != null) AddFromFtrString(line, flightSystem);
    }

    public static void AddFromFtrString(string line, FlightSystem flightSystem)
    {
        var stringCodeToFunction = new Dictionary<string, Action<string>>
        {
            { "C", s => flightSystem.Crew.Add(CrewFactory.CreateFromString(s)) },
            { "P", s => flightSystem.Passengers.Add(PassengerFactory.CreateFromString(s)) },
            { "CA", s => flightSystem.Cargoes.Add(CargoFactory.CreateFromString(s)) },
            { "CP", s => flightSystem.CargoPlanes.Add(CargoPlaneFactory.CreateFromString(s)) },
            { "PP", s => flightSystem.PassengerPlanes.Add(PassengerPlaneFactory.CreateFromString(s)) },
            { "AI", s => flightSystem.Airports.Add(AirportFactory.CreateFromString(s)) },
            { "FL", s => flightSystem.Flights.Add(FlightFactory.CreateFromString(s)) }
        };

        var split = line.Split(',');
        stringCodeToFunction[split[0]](line);
    }
}