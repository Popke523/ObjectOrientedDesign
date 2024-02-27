using System.Text.Json;

namespace ObjectOrientedDesign
{
    internal class Program
    {
        static void Main(string[] args)
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
            List<FlightSystemObject> imported = new List<FlightSystemObject>();

            // Dictionary used to select correct factory based on first 1 or 2 letters of each line of the input file
            Dictionary<string, IFactory> factories = new Dictionary<string, IFactory>() {
                {"C", new CrewFactory() },
                {"P", new PassengerFactory() },
                {"CA", new CargoFactory() },
                {"CP", new CargoPlaneFactory() },
                {"PP", new PassengerPlaneFactory() },
                {"AI", new AirportFactory() },
                {"FL", new FlightFactory() }
            };

            // Parse ftr file line by line and add objects to the list
            using (StreamReader sr = new StreamReader(ftrInputFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split(',');
                    imported.Add(factories[split[0]].CreateFromString(line));
                }
            }

            // Serialize to JSON
            string jsonString = JsonSerializer.Serialize(imported);
            File.WriteAllText(jsonOutputFileName, jsonString);
        }
    }
}
