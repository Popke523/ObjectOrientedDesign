using System.Text.Json.Serialization;
using Mapsui.Projections;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;
using ObjectOrientedDesign.FlightSystem.Reader;

namespace ObjectOrientedDesign.FlightSystem;

public class FlightSystem
{
    public readonly object FsLock = new();

    [JsonInclude] public List<Airport> Airports = [];
    [JsonInclude] public List<Cargo> Cargoes = [];
    [JsonInclude] public List<CargoPlane> CargoPlanes = [];
    [JsonInclude] public List<Crew> Crew = [];
    [JsonInclude] public List<Flight> Flights = [];
    [JsonInclude] public List<PassengerPlane> PassengerPlanes = [];
    [JsonInclude] public List<Passenger> Passengers = [];

    public void OnNewDataReady(object sender, NewDataReadyArgs args)
    {
        var nss = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
        var message = nss.GetMessageAt(args.MessageIndex);
        NetworkSourceMessageReader.AddToFlightSystem(message, this);
    }

    public void UpdateFlightPositions()
    {
        // NOTE: This makes a linear interpolation on the Mercator projection.
        // This is not the shortest path between two points on Earth.
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);
        lock (FsLock)
        {
            foreach (var flight in Flights)
            {
                var origin = Airports.Find(x => x.Id == flight.OriginId);
                var target = Airports.Find(x => x.Id == flight.TargetId);

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

                var timeDiffMs = landingTimeMs - takeoffTimeMs;
                (double x, double y) distance = (targetMercatorCoords.x - originMercatorCoords.x,
                    targetMercatorCoords.y - originMercatorCoords.y);
                var elapsedTime = currentTimeMs - takeoffTimeMs;

                (double x, double y) newMercatorCoords = (
                    elapsedTime / timeDiffMs * distance.x + originMercatorCoords.x,
                    elapsedTime / timeDiffMs * distance.y + originMercatorCoords.y);

                var position = SphericalMercator.ToLonLat(newMercatorCoords.x, newMercatorCoords.y);

                flight.Angle = Math.Atan2(newMercatorCoords.x - currentMercatorCoords.x,
                    newMercatorCoords.y - currentMercatorCoords.y);
                flight.Latitude = (float)position.lat;
                flight.Longitude = (float)position.lon;
            }
        }
    }
}