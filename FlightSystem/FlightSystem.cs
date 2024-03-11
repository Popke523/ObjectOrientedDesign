using System.Text;
using System.Text.Json.Serialization;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem;

public class FlightSystem
{
    public readonly Dictionary<string, Action<Message>> MessageCodeToFunction;
    public readonly Dictionary<string, Action<string>> StringCodeToFunction;

    [JsonInclude] public List<Airport> Airports;
    [JsonInclude] public List<Cargo> Cargoes;
    [JsonInclude] public List<CargoPlane> CargoPlanes;
    [JsonInclude] public List<Crew> Crew;
    [JsonInclude] public List<Flight> Flights;
    [JsonInclude] public List<PassengerPlane> PassengerPlanes;
    [JsonInclude] public List<Passenger> Passengers;


    public FlightSystem()
    {
        Airports = [];
        Cargoes = [];
        CargoPlanes = [];
        Crew = [];
        Flights = [];
        PassengerPlanes = [];
        Passengers = [];

        StringCodeToFunction = new Dictionary<string, Action<string>>
        {
            { "C", s => Crew.Add(Object.Crew.CreateFromString(s)) },
            { "P", s => Passengers.Add(Passenger.CreateFromString(s)) },
            { "CA", s => Cargoes.Add(Cargo.CreateFromString(s)) },
            { "CP", s => CargoPlanes.Add(CargoPlane.CreateFromString(s)) },
            { "PP", s => PassengerPlanes.Add(PassengerPlane.CreateFromString(s)) },
            { "AI", s => Airports.Add(Airport.CreateFromString(s)) },
            { "FL", s => Flights.Add(Flight.CreateFromString(s)) }
        };

        MessageCodeToFunction = new Dictionary<string, Action<Message>>
        {
            { "NCR", message => Crew.Add(Object.Crew.CreateFromMessage(message)) },
            { "NPA", message => Passengers.Add(Passenger.CreateFromMessage(message)) },
            { "NCA", message => Cargoes.Add(Cargo.CreateFromMessage(message)) },
            { "NCP", message => CargoPlanes.Add(CargoPlane.CreateFromMessage(message)) },
            { "NPP", message => PassengerPlanes.Add(PassengerPlane.CreateFromMessage(message)) },
            { "NAI", message => Airports.Add(Airport.CreateFromMessage(message)) },
            { "NFL", message => Flights.Add(Flight.CreateFromMessage(message)) }
        };
    }

    public FlightSystem(string filename) : this()
    {
        using var sr = new StreamReader(filename);
        string? line;
        while ((line = sr.ReadLine()) != null) AddFromFtrString(line);
    }

    public void AddFromFtrString(string s)
    {
        var split = s.Split(',');
        StringCodeToFunction[split[0]](s);
    }

    public void AddFromNetworkSourceMessage(Message message)
    {
        var objectCode = Encoding.ASCII.GetString(message.MessageBytes, 0, 3);
        MessageCodeToFunction[objectCode](message);
    }

    public void OnNewDataReady(object sender, NewDataReadyArgs args)
    {
        Console.WriteLine("Received a message");
        var nss = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
        var message = nss.GetMessageAt(args.MessageIndex);
        AddFromNetworkSourceMessage(message);
    }
}