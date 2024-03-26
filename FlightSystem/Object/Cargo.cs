namespace ObjectOrientedDesign.FlightSystem.Object;

public class Cargo(ulong id, float weight, string code, string description)
    : FlightSystemObject
{
    public ulong Id { get; set; } = id;
    public float Weight { get; set; } = weight;
    public string Code { get; set; } = code;
    public string Description { get; set; } = description;
}