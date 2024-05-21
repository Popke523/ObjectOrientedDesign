using System.Text.RegularExpressions;
using ObjectOrientedDesign.FlightSystem.Query.Conditions;

namespace ObjectOrientedDesign.FlightSystem.Query;

public class DisplayParser
{
    public static Displayer Parse(string s, FlightSystem flightSystem)
    {
        string table;
        string fields;
        string conditions;

        var rg1 = new Regex("display (.*) from (\\w+)(?: where (.*))?", RegexOptions.IgnoreCase);
        var match = rg1.Match(s);

        table = match.Groups[2].Value;
        fields = match.Groups[1].Value;
        conditions = match.Groups[3].Value;

        var fieldsList = fields.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var fullCondition = new OrCondition(conditions
            .Split("or", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => new AndCondition(x
                .Split("and", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(y => new CompareCondition(y)))));

        return new Displayer(table, fieldsList, fullCondition, flightSystem);
    }
}