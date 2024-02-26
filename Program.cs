using System.Text.Json;

namespace ObjectOrientedDesign
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IImport> imported = new List<IImport>();

            Dictionary<string, IFactory> keyValuePairs = new Dictionary<string, IFactory>() {
                {"C", new CrewFactory() },
                {"P", new PassengerFactory() },
                {"CA", new CargoFactory() },
                {"CP", new CargoPlaneFactory() },
                {"PP", new PassengerPlaneFactory() },
                {"AI", new AirportFactory() },
                {"FL", new FlightFactory() }
            };

            using (StreamReader sr = new StreamReader("example_data.ftr"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split(',');
                    imported.Add(keyValuePairs[split[0]].CreateFromString(line));
                }
            }

            string fileName = "serialized.json";
            string jsonString = JsonSerializer.Serialize(imported);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
