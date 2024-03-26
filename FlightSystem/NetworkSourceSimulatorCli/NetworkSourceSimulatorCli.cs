using System.Text.Json;

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
                    string jsonString;
                    lock (flightSystem.FsLock)
                    {
                        jsonString = JsonSerializer.Serialize(flightSystem);
                    }

                    var currentTime = DateTime.Now;
                    var filename = $"snapshot_{currentTime:HH_mm_ss}.json";
                    File.WriteAllText(filename, jsonString);
                    Console.WriteLine($"Snapshot saved as {filename}");
                    break;
                case "exit":
                    exit = 1;
                    break;
            }

        Stop();
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
}