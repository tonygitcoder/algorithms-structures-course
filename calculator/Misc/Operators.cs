namespace calculator.Misc;

public static class Operators
{
    public static bool IsOperator(string opString)
    {
        // LINQ instead of foreach
        return _operators.Any(op => op.Value == opString);
    }
    
    public static bool IsOperator(MathUnit unit)
    {
        return IsOperator(unit.Value);
    }
    
    public static bool IsOperatorish(MathUnit unit)
    {
        return IsOperator(unit.Value) || IsParentheses(unit);
    }
    
    public static bool IsParentheses(MathUnit unit)
    {
        return IsLeftParenthesis(unit) || IsRightParenthesis(unit);
    }
    
    public static bool IsLeftParenthesis(MathUnit unit)
    {
        return unit.Value is "(";
    }
    
    public static bool IsRightParenthesis(MathUnit unit)
    {
        return unit.Value is ")";
    }
    
    public static bool IsInt(MathUnit unit)
    {
        return int.TryParse(unit.Value, out _);
    }
    
    public static bool IsFloat(MathUnit unit)
    {
        return float.TryParse((ReadOnlySpan<char>) unit.Value,
            System.Globalization.NumberStyles.Float
            | System.Globalization.NumberStyles.AllowThousands,
            System.Globalization.NumberFormatInfo.InvariantInfo, 
            out _);
    }
    
    public static bool IsFloatDelimiter(MathUnit unit)
    {
        return unit.Value is "," or ".";
    }

    public static Operator TryToOperator(MathUnit unit)
    {
        foreach (var op in _operators)
        {
            if (op.Value == unit.Value)
            {
                return op;
            }
        }

        throw new Exception($"The Char {unit.Value} can not be converted to an Operator");
    }

    private static readonly List<Operator> _operators = new()
    {
        new Operator("+", 2),
        new Operator("-", 2),
        new Operator("*", 3),
        new Operator("/", 3),
        new Operator("^", 4),
    };
}