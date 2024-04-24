using System.Text.Json.Serialization;
using Mapsui.Projections;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;
using ObjectOrientedDesign.FlightSystem.Reader;

namespace ObjectOrientedDesign.FlightSystem;

public class FlightSystem(Logger.Logger logger)
{
    private readonly Logger.Logger _logger = logger;
    public readonly object FsLock = new();
    [JsonIgnore] public readonly Dictionary<ulong, FlightSystemObject> ObjectIds = new();

    [JsonInclude] public List<Airport> Airports = [];
    [JsonInclude] public List<Cargo> Cargoes = [];
    [JsonInclude] public List<CargoPlane> CargoPlanes = [];
    [JsonInclude] public List<Crew> Crew = [];
    [JsonInclude] public List<Flight> Flights = [];
    [JsonInclude] public List<PassengerPlane> PassengerPlanes = [];
    [JsonInclude] public List<Passenger> Passengers = [];

    [JsonIgnore] public IEnumerable<Person> People => Crew.Concat<Person>(Passengers);

    public void OnNewDataReady(object sender, NewDataReadyArgs args)
    {
        var nss = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
        var message = nss.GetMessageAt(args.MessageIndex);
        NetworkSourceMessageReader.AddToFlightSystem(message, this);
    }

    public void OnIDUpdate(object sender, IDUpdateArgs args)
    {
        var nss = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
        var nsmr = new NetworkSourceMessageReader(_logger);
        nsmr.UpdateId(this, args.ObjectID, args.NewObjectID);
    }


    public void UpdateFlightPositions()
    {
        // NOTE: This makes a linear interpolation using Mercator projection.
        // This is not the shortest path between two points on Earth.
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);
        lock (FsLock)
        {
            foreach (var flight in Flights)
            {
                var origin = flight.Origin;
                var target = flight.Target;

                var currentMercatorCoords =
                    SphericalMercator.FromLonLat(flight.Longitude, flight.Latitude);
                var originMercatorCoords =
                    SphericalMercator.FromLonLat(origin!.Longitude, origin.Latitude);
                var targetMercatorCoords =
                    SphericalMercator.FromLonLat(target!.Longitude, target.Latitude);

                var takeoffTimeMs = TimeOnly.FromDateTime(flight.TakeoffTime).ToTimeSpan().TotalMilliseconds;
                var landingTimeMs = TimeOnly.FromDateTime(flight.LandingTime).ToTimeSpan().TotalMilliseconds;
                var currentTimeMs = currentTime.ToTimeSpan().TotalMilliseconds;

                if (takeoffTimeMs > landingTimeMs)
                {
                    if (currentTimeMs < landingTimeMs) currentTimeMs += 86400000;
                    landingTimeMs += 86400000;
                }

                if (!(takeoffTimeMs < currentTimeMs) || !(currentTimeMs < landingTimeMs)) continue;

                var timeDiffMs = landingTimeMs - currentTimeMs;
                (double x, double y) distance = (targetMercatorCoords.x - currentMercatorCoords.x,
                    targetMercatorCoords.y - currentMercatorCoords.y);
                var elapsedTimeMs = 1000;

                (double x, double y) newMercatorCoords = (
                    elapsedTimeMs / timeDiffMs * distance.x + currentMercatorCoords.x,
                    elapsedTimeMs / timeDiffMs * distance.y + currentMercatorCoords.y);

                var position = SphericalMercator.ToLonLat(newMercatorCoords.x, newMercatorCoords.y);

                flight.Angle = Math.Atan2(newMercatorCoords.x - currentMercatorCoords.x,
                    newMercatorCoords.y - currentMercatorCoords.y);
                flight.Latitude = (float)position.lat;
                flight.Longitude = (float)position.lon;
            }
        }
    }

    public void OnPositionUpdate(object sender, PositionUpdateArgs args)
    {
        var nss = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
        var nsmr = new NetworkSourceMessageReader(_logger);
        nsmr.UpdatePosition(this, args.ObjectID, args.Latitude, args.Longitude, args.AMSL);
    }

    public void OnContactInfoUpdate(object sender, ContactInfoUpdateArgs args)
    {
        var nss = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
        var nsmr = new NetworkSourceMessageReader(_logger);
        nsmr.UpdateContactInfo(this, args.ObjectID, args.PhoneNumber, args.EmailAddress);
    }
}