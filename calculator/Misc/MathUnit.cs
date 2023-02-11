namespace calculator.Misc;

public class MathUnit
{
    public readonly string Value;

    public MathUnit(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
    
    public static MathUnit ToOperatorish(MathUnit unit) => new Operatorish(unit.Value);
}