namespace ObjectOrientedDesign.FlightSystem.Factory;

public interface IFactory
{
    public FlightSystemObject CreateFromString(string s);
}