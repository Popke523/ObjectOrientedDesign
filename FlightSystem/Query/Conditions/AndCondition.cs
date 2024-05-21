namespace ObjectOrientedDesign.FlightSystem.Query.Conditions;

public class AndCondition(IEnumerable<Condition> conditions) : Condition
{
    public override bool Validate(TableRow row)
    {
        return conditions.All(x => x.Validate(row));
    }
}