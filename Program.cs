using Microsoft.Extensions.Configuration;
using ObjectOrientedDesign.FlightSystem.FlightGUIAdapter;
using ObjectOrientedDesign.FlightSystem.NetworkSourceSimulatorCli;
using ObjectOrientedDesign.FlightSystem.Reader;

namespace ObjectOrientedDesign;

internal class Program
{
    private static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var section = config.GetSection("Settings");

        FlightSystem.FlightSystem flightSystem;

        var ftrInputFileName = section["FTRInputFileName"] ?? "data/example.json";
        switch (section["Mode"])
        {
            case "ReadFromFTRFile":
            {
                using var streamReader = new StreamReader(ftrInputFileName);
                flightSystem = new FtrDataReader(streamReader).ToFlightSystem();
                break;
            }

            case "NetworkSimulator":
            {
                flightSystem = new FlightSystem.FlightSystem();
                var minOffsetMs = int.Parse(section["NetworkSimulatorMinMessageOffset"] ?? "100");
                var maxOffsetMs = int.Parse(section["NetworkSimulatorMaxMessageOffset"] ?? "200");
                var networkSourceSimulator =
                    new NetworkSourceSimulator.NetworkSourceSimulator(ftrInputFileName, minOffsetMs, maxOffsetMs);
                networkSourceSimulator.OnNewDataReady += flightSystem.OnNewDataReady;
                var nssTaskCancellationTokenSource = new CancellationTokenSource();
                var nssTask = Task.Run(networkSourceSimulator.Run, nssTaskCancellationTokenSource.Token);
                var cli = new NetworkSourceSimulatorCli(networkSourceSimulator, flightSystem, nssTask,
                    nssTaskCancellationTokenSource);
                Task.Run(cli.Run);
                break;
            }
            default:
                throw new Exception("Invalid mode read from config file");
        }

        var gui = new FlightSystemToGuiAdapter(flightSystem);

        Task.Run(FlightSystemToGuiAdapter.Run);

        while (true)
        {
            flightSystem.UpdateFlightPositions();
            gui.UpdateGui();
            Thread.Sleep(1000 - DateTime.Now.Millisecond);
        }
    }
}