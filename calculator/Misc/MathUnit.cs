namespace calculator.Misc;

public class MathUnit
{
    public readonly string Value;
    public int TryParseInt => int.TryParse(this.Value, out var result) ? result : 0;
    public float TryParseFloat => float.TryParse(this.Value, out var result) ? result : 0f;

    public MathUnit(string value)
    {
        Value = value;
    }
    
    public MathUnit(int value)
    {
        Value = value.ToString();
    }
    
    public MathUnit(float value)
    {
        Value = value.ToString();
    }

    public MathUnit(double value)
    {
        Value = value.ToString();
    }

    public override string ToString()
    {
        return Value;
    }
    
    public static MathUnit ToOperatorish(MathUnit unit) => new Operatorish(unit.Value);
}