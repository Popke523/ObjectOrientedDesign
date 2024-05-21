using ObjectOrientedDesign.FlightSystem.Query.Conditions;

namespace ObjectOrientedDesign.FlightSystem.Query;

public class Displayer(string table, IEnumerable<string> fields, Condition condition, FlightSystem flightSystem)
{
    public readonly Condition Condition = condition;
    public string[] Fields = fields.ToArray();

    public Dictionary<string, IEnumerable<TableRow>> Fs = new()
    {
        { "airports", flightSystem.Airports.Select(x => new AirportTableRow(x)) }
    };

    public readonly string Table = table;

    public void Display()
    {
        foreach (var row in RowsFilter.Filter(Fs[Table], Condition))
        {
            foreach (var field in fields) Console.Write(row.Get(field));
            Console.WriteLine();
        }
    }
}