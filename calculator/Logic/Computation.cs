namespace calculator.Logic;

using Misc;
public class Computation
{
    public static Stack<MathUnit> ComputeOutput(Queue<MathUnit> input)
    {
        var numberStack = new Stack<MathUnit>();

        foreach (MathUnit symbol in input)
        {
            if (Operators.IsFloat(symbol))
            {
                numberStack.Push(symbol);
            }

            if (Operators.IsOperatorish(symbol))
            {
                var lastInStack = numberStack.Pop();
                var secondLastInStack = numberStack.Pop();
                var result = Operators.Evaluate(secondLastInStack, lastInStack, symbol);
                numberStack.Push(result);
            }
        }
        return numberStack;
    } 
}