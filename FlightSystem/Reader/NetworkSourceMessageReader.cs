using System.Text;
using NetworkSourceSimulator;
using ObjectOrientedDesign.FlightSystem.Factory;
using ObjectOrientedDesign.FlightSystem.Object;

namespace ObjectOrientedDesign.FlightSystem.Reader;

public class NetworkSourceMessageReader(Logger.Logger logger)
{
    private readonly Logger.Logger _logger = logger;

    public static void AddToFlightSystem(Message message, FlightSystem flightSystem)
    {
        var messageCodeToFunction = new Dictionary<string, Action<Message>>
        {
            {
                "NCR", message =>
                {
                    var fromMessage = CrewFactory.CreateFromMessage(message);
                    if (flightSystem.ObjectIds.ContainsKey(fromMessage.Id))
                        throw new Exception($"Object with id ${fromMessage.Id} already exists in the flight system!");
                    flightSystem.Crew.Add(fromMessage);
                }
            },
            {
                "NPA", message =>
                {
                    var fromMessage = PassengerFactory.CreateFromMessage(message);
                    if (flightSystem.ObjectIds.ContainsKey(fromMessage.Id))
                        throw new Exception($"Object with id ${fromMessage.Id} already exists in the flight system!");
                    flightSystem.Passengers.Add(fromMessage);
                }
            },
            {
                "NCA", message =>
                {
                    var fromMessage = CargoFactory.CreateFromMessage(message);
                    if (flightSystem.ObjectIds.ContainsKey(fromMessage.Id))
                        throw new Exception($"Object with id ${fromMessage.Id} already exists in the flight system!");
                    flightSystem.Cargoes.Add(fromMessage);
                }
            },
            {
                "NCP", message =>
                {
                    var fromMessage = CargoPlaneFactory.CreateFromMessage(message);
                    if (flightSystem.ObjectIds.ContainsKey(fromMessage.Id))
                        throw new Exception($"Object with id ${fromMessage.Id} already exists in the flight system!");
                    flightSystem.CargoPlanes.Add(fromMessage);
                }
            },
            {
                "NPP", message =>
                {
                    var fromMessage = PassengerPlaneFactory.CreateFromMessage(message);
                    if (flightSystem.ObjectIds.ContainsKey(fromMessage.Id))
                        throw new Exception($"Object with id ${fromMessage.Id} already exists in the flight system!");
                    flightSystem.PassengerPlanes.Add(fromMessage);
                }
            },
            {
                "NAI", message =>
                {
                    var fromMessage = AirportFactory.CreateFromMessage(message);
                    if (flightSystem.ObjectIds.ContainsKey(fromMessage.Id))
                        throw new Exception($"Object with id ${fromMessage.Id} already exists in the flight system!");
                    flightSystem.Airports.Add(fromMessage);
                }
            },
            {
                "NFL", message =>
                {
                    var fromMessage = FlightFactory.CreateFromMessage(message, flightSystem);
                    if (flightSystem.ObjectIds.ContainsKey(fromMessage.Id))
                        throw new Exception($"Object with id ${fromMessage.Id} already exists in the flight system!");
                    flightSystem.Flights.Add(fromMessage);
                }
            }
        };

        var objectCode = Encoding.ASCII.GetString(message.MessageBytes, 0, 3);
        lock (flightSystem.FsLock)
        {
            messageCodeToFunction[objectCode](message);
        }
    }

    public void UpdateId(FlightSystem flightSystem, ulong from, ulong to)
    {
        if (from == to) return;
        if (!flightSystem.ObjectIds.ContainsKey(from))
        {
            _logger.Log($"Object with id ${to} already exists in the flight system!");
            return;
        }

        var obj = flightSystem.ObjectIds[from];
        flightSystem.ObjectIds[to] = obj;
        obj.Id = to;
        flightSystem.ObjectIds.Remove(from);
        _logger.Log($"Changed object ID from {from} to {to}");
    }

    public void UpdatePosition(FlightSystem flightSystem, ulong objectId, float latitude, float longitude)
    {
        var flight = flightSystem.Flights.FirstOrDefault(x=> x!.Id == objectId, null);
        if (flight is null)
        {
            _logger.Log($"Flight with id ${objectId} does not exist in the flight system!");
            return;
        }
        
        flight.Latitude = latitude;
        flight.Longitude = longitude;
        _logger.Log($"Position of flight with id ${objectId} updated to ({latitude}, {longitude})");
    }

    public void UpdateContactInfo(FlightSystem flightSystem, ulong objectId, string phoneNumber, string emailAddress)
    {
        var person = flightSystem.People.FirstOrDefault(x => x!.Id == objectId, null);
        if (person is null)
        {
            _logger.Log($"Person with id ${objectId} does not exist in the flight system!");
            return;
        }

        var oldPhone = person.Phone;
        var oldEmail = person.Email;
        person.Phone = phoneNumber;
        person.Email = emailAddress;
        _logger.Log(
            $"Contact info of object with id {objectId} changed from {oldPhone}, {oldEmail} to {phoneNumber}, {emailAddress}");
    }
}