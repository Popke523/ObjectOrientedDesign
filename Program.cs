using FlightTrackerGUI;
using Mapsui.Projections;

namespace ObjectOrientedDesign;

internal class Program
{
    private static int _i;

    private static TimeOnly _currentTime = TimeOnly.FromDateTime(DateTime.Now);

    private static void Main(string[] args)
    {
        // Read filenames from arguments or use defaults
        string ftrInputFileName;
        int minOffsetInMs;
        int maxOffsetInMs;
        switch (args.Length)
        {
            case 3:
                ftrInputFileName = args[0];
                minOffsetInMs = int.Parse(args[1]);
                maxOffsetInMs = int.Parse(args[2]);
                break;
            case 2:
                ftrInputFileName = args[0];
                minOffsetInMs = int.Parse(args[1]);
                maxOffsetInMs = 200;
                break;
            case 1:
                ftrInputFileName = args[0];
                minOffsetInMs = 100;
                maxOffsetInMs = 200;
                break;
            default:
                ftrInputFileName = "data/example_data.ftr";
                minOffsetInMs = 100;
                maxOffsetInMs = 200;
                break;
        }

        var flightSystem = new FlightSystem.FlightSystem(ftrInputFileName);

        Task.Run(Runner.Run);

        while (true)
        {
            UpdateGui(flightSystem);
            Thread.Sleep(10);
        }
    }

    private static void UpdateGui(FlightSystem.FlightSystem flightSystem)
    {
        var flightGuiList = new List<FlightGUI>();

        var datetime = DateTime.Now;


        foreach (var flight in flightSystem.Flights)
        {
            var origin = flightSystem.Airports.Find(x => x.Id == flight.OriginID);
            var target = flightSystem.Airports.Find(x => x.Id == flight.TargetID);

            var originMercatorCoords =
                SphericalMercator.FromLonLat(origin!.Longitude, origin.Latitude);
            var targetMercatorCoords =
                SphericalMercator.FromLonLat(target!.Longitude, target.Latitude);

            var angle = Math.Atan2(targetMercatorCoords.x - originMercatorCoords.x,
                targetMercatorCoords.y - originMercatorCoords.y);

            var takeoffTimeMs = TimeOnly.FromDateTime(flight.TakeoffTime).ToTimeSpan().TotalMilliseconds;
            var landingTimeMs = TimeOnly.FromDateTime(flight.LandingTime).ToTimeSpan().TotalMilliseconds;
            var currentTimeMs = _currentTime.ToTimeSpan().TotalMilliseconds;

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

            var position = SphericalMercator.ToLonLat(elapsedTime / timeDiffMs * distance.x + originMercatorCoords.x,
                elapsedTime / timeDiffMs * distance.y + originMercatorCoords.y);

            flightGuiList.Add(new FlightGUI
            {
                ID = flight.ID, MapCoordRotation = angle, WorldPosition = new WorldPosition(position.lat, position.lon)
            });
        }


        FlightsGUIData flightsGuiData;

        flightsGuiData = new FlightsGUIData(flightGuiList);
        
        Runner.UpdateGUI(flightsGuiData);
        _currentTime = _currentTime.AddMinutes(1);
    }
}

// TODO: Move to another class
// var nss = new NetworkSourceSimulator.NetworkSourceSimulator(ftrInputFileName, minOffsetInMs, maxOffsetInMs);
//
// nss.OnNewDataReady += fs.OnNewDataReady;
//
// var nssTaskCancellationTokenSource = new CancellationTokenSource();
// var nssTask = Task.Run(nss.Run, nssTaskCancellationTokenSource.Token);
//
// var exit = 0;
// while (exit == 0)
//     switch (Console.ReadLine())
//     {
//         case "print":
//             string jsonString;
//             lock (fs.FsLock)
//             {
//                 jsonString = JsonSerializer.Serialize(fs);
//             }
//
//             var currentTime = DateTime.Now;
//             File.WriteAllText($"snapshot_{currentTime:HH_mm_ss}.json", jsonString);
//             break;
//         case "exit":
//             exit = 1;
//             break;
//     }
//
// nssTaskCancellationTokenSource.Cancel();
// try
// {
//     nssTask.Wait(nssTaskCancellationTokenSource.Token);
// }
// catch (OperationCanceledException e)
// {
//     Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
// }
// finally
// {
//     nssTaskCancellationTokenSource.Dispose();
// }

// main