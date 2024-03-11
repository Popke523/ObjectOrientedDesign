using System.Text.Json;
using ObjectOrientedDesign.FlightSystem.Factory;

namespace ObjectOrientedDesign;

internal class Program
{
    private static void Main(string[] args)
    {
        // Read filenames from arguments or use defaults
        string ftrInputFileName;
        string jsonOutputFileName;
        switch (args.Length)
        {
            case 2:
                ftrInputFileName = args[0];
                jsonOutputFileName = args[1];
                break;
            case 1:
                ftrInputFileName = args[0];
                jsonOutputFileName = "example_data.json";
                break;
            default:
                ftrInputFileName = "example_data.ftr";
                jsonOutputFileName = "example_data.json";
                break;
        }

        // List of imported objects to serialize later
        var imported = new List<FlightSystemObject>();

        // Dictionary used to select correct factory based on first 1 or 2 letters of each line of the input file
        var factories = new Dictionary<string, IFactory>
        {
            { "C", new CrewFactory() },
            { "P", new PassengerFactory() },
            { "CA", new CargoFactory() },
            { "CP", new CargoPlaneFactory() },
            { "PP", new PassengerPlaneFactory() },
            { "AI", new AirportFactory() },
            { "FL", new FlightFactory() }
        };

        // Parse ftr file line by line and add objects to the list
        using (var sr = new StreamReader(ftrInputFileName))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var split = line.Split(',');
                imported.Add(factories[split[0]].CreateFromString(line));
            }
        }

        // Serialize to JSON
        var jsonString = JsonSerializer.Serialize(imported);
        File.WriteAllText(jsonOutputFileName, jsonString);


        var nss = new NetworkSourceSimulator.NetworkSourceSimulator(ftrInputFileName, 100, 200);
    }
}