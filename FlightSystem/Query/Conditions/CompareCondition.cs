using System.Collections;
using System.Globalization;

namespace ObjectOrientedDesign.FlightSystem.Query.Conditions;

public class CompareCondition : Condition
{
    private readonly bool[] _allowed;
    public string RowElement;
    public string Value;

    public CompareCondition(string s)
    {
        var split = s.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        RowElement = split[0];
        var compareSign = split[1];
        Value = split[2];

        _allowed = [false, false, false];

        _allowed[0] = compareSign is "<" or "<=" or "!=";
        _allowed[1] = compareSign is "=" or "<=" or ">=";
        _allowed[2] = compareSign is ">" or ">=" or "!=";
    }

    public override bool Validate(TableRow row)
    {
        var comparer = new Comparer(CultureInfo.InvariantCulture);
        return _allowed[1 + comparer.Compare(row.Get(RowElement.ToLower()), row.Parsers[RowElement](Value))];
    }
}