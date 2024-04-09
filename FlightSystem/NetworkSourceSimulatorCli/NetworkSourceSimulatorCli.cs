using System.Text.Json;
using ObjectOrientedDesign.FlightSystem.News;
using ObjectOrientedDesign.FlightSystem.News.Media;

namespace ObjectOrientedDesign.FlightSystem.NetworkSourceSimulatorCli;

public class NetworkSourceSimulatorCli(
    NetworkSourceSimulator.NetworkSourceSimulator networkSourceSimulator,
    FlightSystem flightSystem,
    Task nssTask,
    CancellationTokenSource nssTaskCancellationTokenSource)
{
    public NetworkSourceSimulator.NetworkSourceSimulator NetworkSourceSimulator = networkSourceSimulator;

    public void Run()
    {
        var exit = 0;
        while (exit == 0)
            switch (Console.ReadLine())
            {
                case "print":
                    Print();
                    break;
                case "report":
                    Report();
                    break;
                case "exit":
                    exit = 1;
                    break;
            }

        Stop();
    }

    private void Report()
    {
        List<Medium> media =
        [
            new Television("Telewizja Abelowa"),
            new Television("Kanał TV-tensor"),
            new Radio("Radio Kwantyfikator"),
            new Radio("Radio Shmem"),
            new Newspaper("Gazeta Kategoryczna"),
            new Newspaper("Dziennik Politechniczny")
        ];

        List<IReportable> reportables;
        lock (flightSystem.FsLock)
        {
            reportables = flightSystem.Airports.Concat<IReportable>(flightSystem.CargoPlanes)
                .Concat(flightSystem.PassengerPlanes).ToList();
        }

        var newsGenerator = new NewsGenerator(media, reportables);

        string? s;
        while ((s = newsGenerator.GenerateNextNews()) != null) Console.WriteLine(s);
    }

    private void Stop()
    {
        nssTaskCancellationTokenSource.Cancel();
        try
        {
            nssTask.Wait(nssTaskCancellationTokenSource.Token);
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
        }
        finally
        {
            nssTaskCancellationTokenSource.Dispose();
        }
    }

    private void Print()
    {
        string jsonString;
        lock (flightSystem.FsLock)
        {
            jsonString = JsonSerializer.Serialize(flightSystem);
        }

        var currentTime = DateTime.Now;
        var filename = $"snapshot_{currentTime:HH_mm_ss}.json";
        File.WriteAllText(filename, jsonString);
        Console.WriteLine($"Snapshot saved as {filename}");
    }
}