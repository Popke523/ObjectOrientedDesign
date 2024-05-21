using ObjectOrientedDesign.FlightSystem.Query.Conditions;

namespace ObjectOrientedDesign.FlightSystem.Query;

public static class RowsFilter
{
    public static IEnumerable<TableRow> Filter(IEnumerable<TableRow> rows, Condition condition)
    {
        return rows.Where(condition.Validate);
    }
}