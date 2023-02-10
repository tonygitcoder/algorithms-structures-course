namespace calculator;

public static class Operators
{
    public static bool IsOperator(char opChar)
    {
        foreach (var op in _operators)
        {
            if (op.Symbol == opChar)
            {
                return true;
            }
        }
        
        return false;
    }

    private static Operator ToOperator(char opChar)
    {
        foreach (var op in _operators)
        {
            if (op.Symbol == opChar)
            {
                return op;
            }
        }
        
        throw new System.Exception("The Char can not be converted to an Operator");
    }

    private static readonly List<Operator> _operators = new List<Operator> {
        new Operator('+', 2),
        new Operator('-', 2),
        new Operator('*', 3),
        new Operator('/', 3),
        new Operator('^', 4),
    };
}