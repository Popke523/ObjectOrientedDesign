namespace ObjectOrientedDesign.FlightSystem.Query.Conditions;

public class OrCondition(IEnumerable<Condition> conditions) : Condition
{
    public override bool Validate(TableRow row)
    {
        return !conditions.Any() || conditions.Any(x => x.Validate(row));
    }
}