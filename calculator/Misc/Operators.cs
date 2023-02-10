namespace calculator.Misc;

public static class Operators
{
    public static bool IsOperator(string opString)
    {
        // LINQ instead of foreach
        return _operators.Any(op => op.Symbol == opString);
    }
    
    public static bool IsOperator(MathUnit unit)
    {
        return IsOperator(unit.Value);
    }
    
    public static bool IsParenthesis(MathUnit unit)
    {
        return unit.Value is "(" or ")";
    }
    
    public static bool IsInt(MathUnit unit)
    {
        return int.TryParse(unit.Value, out _);
    }
    
    public static bool IsFloatDelimiter(MathUnit unit)
    {
        return unit.Value == "," || unit.Value == ".";
    }

    public static Operator TryToOperator(MathUnit unit)
    {
        foreach (var op in _operators)
        {
            if (op.Symbol == unit.Value)
            {
                return op;
            }
        }
        
        throw new Exception("The Char can not be converted to an Operator");
    }

    private static readonly List<Operator> _operators = new()
    {
        new Operator("+", 2),
        new Operator("-", 2),
        new Operator("*", 3),
        new Operator("/", 3),
        new Operator("^", 4)
    };
}