using System.Text.Json;

namespace ObjectOrientedDesign;

internal class Program
{
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

        var fs = new FlightSystem.FlightSystem();

        var nss = new NetworkSourceSimulator.NetworkSourceSimulator(ftrInputFileName, minOffsetInMs, maxOffsetInMs);

        nss.OnNewDataReady += fs.OnNewDataReady;

        var nssTaskCancellationTokenSource = new CancellationTokenSource();
        var nssTask = Task.Run(nss.Run, nssTaskCancellationTokenSource.Token);

        var exit = 0;
        while (exit == 0)
            switch (Console.ReadLine())
            {
                case "print":
                    string jsonString;
                    lock (fs.FsLock)
                    {
                        jsonString = JsonSerializer.Serialize(fs);
                    }

                    var currentTime = DateTime.Now;
                    File.WriteAllText($"snapshot_{currentTime:HH_mm_ss}.json", jsonString);
                    break;
                case "exit":
                    exit = 1;
                    break;
            }

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