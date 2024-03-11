namespace ObjectOrientedDesign.FlightSystem.Object;

public class Cargo : FlightSystemObject
{
    public Cargo(ulong id, float weight, string code, string description)
    {
        ID = id;
        Weight = weight;
        Code = code;
        Description = description;
    }

    public ulong ID { get; set; }
    public float Weight { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
}