using System.Text.Json;

namespace ObjectOrientedDesign
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            List<FlightSystemObject> imported = new List<FlightSystemObject>();

            Dictionary<string, IFactory> factories = new Dictionary<string, IFactory>() {
                {"C", new CrewFactory() },
                {"P", new PassengerFactory() },
                {"CA", new CargoFactory() },
                {"CP", new CargoPlaneFactory() },
                {"PP", new PassengerPlaneFactory() },
                {"AI", new AirportFactory() },
                {"FL", new FlightFactory() }
            };

            using (StreamReader sr = new StreamReader(ftrInputFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split(',');
                    imported.Add(factories[split[0]].CreateFromString(line));
                }
            }

            string jsonString = JsonSerializer.Serialize(imported);
            File.WriteAllText(jsonOutputFileName, jsonString);
        }
    }
}
