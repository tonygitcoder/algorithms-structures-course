namespace calculator;


public class Operator : MathUnit
{
    public char Symbol { get; private set; }
    public int Precedence { get; private set; }
    
    // internal = access across the assembly
    internal Operator(char op, int precedence)
    {
        this.Symbol = op;
            this.Precedence = precedence; 
    }
}