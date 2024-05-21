namespace ObjectOrientedDesign.FlightSystem.Query.Conditions;

public abstract class Condition
{
    public abstract bool Validate(TableRow row);
}