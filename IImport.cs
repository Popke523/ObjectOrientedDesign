using System.Text.Json.Serialization;

namespace ObjectOrientedDesign
{
    [JsonDerivedType(typeof(Airport), "AI")]
    [JsonDerivedType(typeof(Cargo), "CA")]
    [JsonDerivedType(typeof(CargoPlane), "CP")]
    [JsonDerivedType(typeof(Crew), "C")]
    [JsonDerivedType(typeof(Flight), "FL")]
    [JsonDerivedType(typeof(Passenger), "P")]
    [JsonDerivedType(typeof(PassengerPlane), "PP")]
    public interface IImport
    {

    }
}