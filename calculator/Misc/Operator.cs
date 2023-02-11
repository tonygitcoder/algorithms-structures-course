namespace calculator.Misc;


public class Operator : MathUnit
{
    public string Symbol { get; private set; }
    public int Precedence { get; private set; }
    
    public Operator(string op, int precedence) : base(op)
    {
        Symbol = op;
        Precedence = precedence; 
    }
}