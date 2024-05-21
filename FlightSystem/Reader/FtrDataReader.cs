using System.Globalization;
using ObjectOrientedDesign.FlightSystem.Factory;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Reader;

public class FtrDataReader(StreamReader streamReader, Logger.Logger logger)
{
    private readonly List<string> _flightsStrings = [];
    private StreamReader StreamReader { get; } = streamReader;

    public FlightSystem ToFlightSystem()
    {
        var flightSystem = new FlightSystem(logger);
        AddToFlightSystem(flightSystem);
        return flightSystem;
    }

    private void AddToFlightSystem(FlightSystem flightSystem)
    {
        string? line;
        while ((line = StreamReader.ReadLine()) != null) AddFromFtrStringFirstPass(line, flightSystem);
        foreach (var s in _flightsStrings) AddFromFtrStringSecondPass(s, flightSystem);
    }

    public void AddFromFtrStringFirstPass(string line, FlightSystem flightSystem)
    {
        var split = line.Split(',');
        var id = ulong.Parse(split[1]);

        if (flightSystem.ObjectIds.ContainsKey(id))
            throw new Exception($"Object with id ${id} already exists in the flight system!");

        var stringCodeToFunction = new Dictionary<string, Action<string>>
        {
            {
                "C", s =>
                {
                    var fromString = CrewFactory.CreateFromString(s);
                    flightSystem.Crew.Add(fromString);
                    flightSystem.ObjectIds.Add(id, fromString);
                }
            },
            {
                "P", s =>
                {
                    var fromString = PassengerFactory.CreateFromString(s);
                    flightSystem.Passengers.Add(fromString);
                    flightSystem.ObjectIds.Add(id, fromString);
                }
            },
            {
                "CA", s =>
                {
                    var fromString = CargoFactory.CreateFromString(s);
                    flightSystem.Cargoes.Add(fromString);
                    flightSystem.ObjectIds.Add(id, fromString);
                }
            },
            {
                "CP", s =>
                {
                    var fromString = CargoPlaneFactory.CreateFromString(s);
                    flightSystem.CargoPlanes.Add(fromString);
                    flightSystem.ObjectIds.Add(id, fromString);
                }
            },
            {
                "PP", s =>
                {
                    var fromString = PassengerPlaneFactory.CreateFromString(s);
                    flightSystem.PassengerPlanes.Add(fromString);
                    flightSystem.ObjectIds.Add(id, fromString);
                }
            },
            {
                "AI", s =>
                {
                    var fromString = AirportFactory.CreateFromString(s);
                    flightSystem.Airports.Add(fromString);
                    flightSystem.ObjectIds.Add(id, fromString);
                }
            },
            { "FL", s => _flightsStrings.Add(s) }
        };


        stringCodeToFunction[split[0]](line);
    }

    public void AddFromFtrStringSecondPass(string line, FlightSystem flightSystem)
    {
        var nfi = NumberFormatInfo.InvariantInfo;
        var split = line.Split(',');
        var id = ulong.Parse(split[1]);

        Airport origin;
        Airport target;
        Plane plane;
        List<Crew> crew = [];
        List<ILoadable> load = [];

        var originId = ulong.Parse(split[2], nfi);
        var targetId = ulong.Parse(split[3], nfi);
        var planeId = ulong.Parse(split[9], nfi);
        var crewIds = Array.ConvertAll(split[10].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse);
        var loadIds = Array.ConvertAll(split[11].TrimStart('[').TrimEnd(']').Split(';'), ulong.Parse);

        if (!flightSystem.ObjectIds.ContainsKey(originId)) throw new Exception($"originId not found: {originId}");
        origin = (Airport)flightSystem.ObjectIds[originId];
        if (!flightSystem.ObjectIds.ContainsKey(targetId)) throw new Exception($"targetId not found: {targetId}");
        target = (Airport)flightSystem.ObjectIds[targetId];
        if (!flightSystem.ObjectIds.ContainsKey(planeId)) throw new Exception($"planeId not found: {planeId}");
        plane = (Plane)flightSystem.ObjectIds[planeId];

        foreach (var x in crewIds)
        {
            if (!flightSystem.ObjectIds.ContainsKey(x))
                throw new Exception($"crewId not found: {x}");
            crew.Add((Crew)flightSystem.ObjectIds[x]);
        }

        foreach (var x in loadIds)
        {
            if (!flightSystem.ObjectIds.ContainsKey(x))
                throw new Exception($"loadId not found: {x}");
            load.Add((ILoadable)flightSystem.ObjectIds[x]);
        }

        var flight = FlightFactory.CreateFromString2(line, origin, target, plane, crew, load);
        flightSystem.Flights.Add(flight);
        flightSystem.ObjectIds.Add(id, flight);
    }
}