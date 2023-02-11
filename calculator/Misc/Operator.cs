namespace calculator.Misc;


public class Operator : Operatorish
{
    public int Precedence { get; private set; }
    
    public Operator(string op, int precedence) : base(op)
    {
        Precedence = precedence; 
    }
}