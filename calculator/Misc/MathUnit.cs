namespace calculator.Misc;

public class MathUnit
{
    public readonly string Value;
    public readonly int Precedence;
    
    public MathUnit(string value, int precedence = 0)
    {
        Value = value;
        Precedence = precedence;
    }

    public override string ToString()
    {
        return Value;
    }
}