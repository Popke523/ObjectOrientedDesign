using System.Text.Json;

namespace ObjectOrientedDesign;

internal class Program
{
    private static void Main(string[] args)
    {
        // Read filenames from arguments or use defaults
        string ftrInputFileName;
        switch (args.Length)
        {
            case 1:
                ftrInputFileName = args[0];
                break;
            default:
                ftrInputFileName = "data/example_data.ftr";
                break;
        }

        var fs = new FlightSystem.FlightSystem();

        var nss = new NetworkSourceSimulator.NetworkSourceSimulator(ftrInputFileName, 100, 200);

        nss.OnNewDataReady += fs.OnNewDataReady;

        var nssTaskCancellationTokenSource = new CancellationTokenSource();
        var nssTask = Task.Run(nss.Run, nssTaskCancellationTokenSource.Token);

        var exit = 0;
        while (exit == 0)
            switch (Console.ReadLine())
            {
                case "print":
                    var jsonString = JsonSerializer.Serialize(fs);
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