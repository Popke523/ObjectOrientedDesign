using FlightTrackerGUI;

namespace ObjectOrientedDesign.FlightSystem.FlightGUIAdapter;

public class FlightSystemToGuiAdapter(FlightSystem flightSystem)
{
    public static void Run()
    {
        Runner.Run();
    }

    public void UpdateGui()
    {
        var flightGuiList = new List<FlightGUI>();
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        lock (flightSystem.FsLock)
        {
            foreach (var flight in flightSystem.Flights)
            {
                var takeoffTimeMs = TimeOnly.FromDateTime(flight.TakeoffTime).ToTimeSpan().TotalMilliseconds;
                var landingTimeMs = TimeOnly.FromDateTime(flight.LandingTime).ToTimeSpan().TotalMilliseconds;
                var currentTimeMs = currentTime.ToTimeSpan().TotalMilliseconds;

                if (takeoffTimeMs > landingTimeMs)
                {
                    if (currentTimeMs < landingTimeMs) currentTimeMs += 86400000;
                    landingTimeMs += 86400000;
                }

                if (!(takeoffTimeMs < currentTimeMs) || !(currentTimeMs < landingTimeMs)) continue;

                flightGuiList.Add(new FlightGUI
                {
                    ID = flight.Id, MapCoordRotation = flight.Angle,
                    WorldPosition = new WorldPosition(flight.Latitude, flight.Longitude)
                });
            }
        }

        var flightsGuiData = new FlightsGUIData(flightGuiList);
        Runner.UpdateGUI(flightsGuiData);
    }
}