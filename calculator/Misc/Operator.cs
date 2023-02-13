namespace calculator.Misc;


public class Operator : Operatorish
{
    public int Precedence { get; private set; }
    private Func<MathUnit, MathUnit, MathUnit> _action;
    public MathUnit Execute(MathUnit left, MathUnit right) => _action(left, right);

    public Operator(string op, int precedence, Func<MathUnit, MathUnit, MathUnit> action) : base(op)
    {
        Precedence = precedence;
        this._action = action;
    }
    
}