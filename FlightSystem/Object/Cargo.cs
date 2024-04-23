namespace ObjectOrientedDesign.FlightSystem.Object;

public class Cargo(ulong id, float weight, string code, string description)
    : FlightSystemObject(id), ILoadable
{
    public float Weight { get; set; } = weight;
    public string Code { get; set; } = code;
    public string Description { get; set; } = description;
}