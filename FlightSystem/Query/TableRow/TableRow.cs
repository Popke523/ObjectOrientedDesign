namespace ObjectOrientedDesign.FlightSystem.Query;

public abstract class TableRow
{
    public Dictionary<string, Func<string, IComparable>> Parsers;

    public abstract IComparable Get(string name);
}