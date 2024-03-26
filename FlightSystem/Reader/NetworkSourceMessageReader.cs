using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Factory;

namespace ObjectOrientedDesign.FlightSystem.Reader;

public class NetworkSourceMessageReader
{
    public static void AddToFlightSystem(Message message, FlightSystem flightSystem)
    {
        var messageCodeToFunction = new Dictionary<string, Action<Message>>
        {
            { "NCR", message => flightSystem.Crew.Add(CrewFactory.CreateFromMessage(message)) },
            { "NPA", message => flightSystem.Passengers.Add(PassengerFactory.CreateFromMessage(message)) },
            { "NCA", message => flightSystem.Cargoes.Add(CargoFactory.CreateFromMessage(message)) },
            { "NCP", message => flightSystem.CargoPlanes.Add(CargoPlaneFactory.CreateFromMessage(message)) },
            { "NPP", message => flightSystem.PassengerPlanes.Add(PassengerPlaneFactory.CreateFromMessage(message)) },
            { "NAI", message => flightSystem.Airports.Add(AirportFactory.CreateFromMessage(message)) },
            { "NFL", message => flightSystem.Flights.Add(FlightFactory.CreateFromMessage(message)) }
        };

        var objectCode = Encoding.ASCII.GetString(message.MessageBytes, 0, 3);
        lock (flightSystem.FsLock)
        {
            messageCodeToFunction[objectCode](message);
        }
    }
}