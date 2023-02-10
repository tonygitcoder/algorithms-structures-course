namespace calculator.Logic;

using Misc;

public static class PostfixConverter
{
    // TODO: Add parenthesis support?
    public static Queue<MathUnit> ConvertToPostfix(List<MathUnit> input)
    {
        // introducing stack and queue
        var operatorStack = new Stack<Operator>();
        var outputQueue = new Queue<MathUnit>();
        
        //loping through each token
        foreach (MathUnit token in input) 
        {
            if (Operators.IsNumeric(token))
            {
                outputQueue.Enqueue(token);
            }
            
            else if (Operators.IsOperator(token)) 
            {
                // there is an operator o2 at the top of the operator stack
                // which is not a left parenthesis, 
                // and (o2 has greater precedence than o1
                // or (o1 and o2 have the same precedence and o1 is left-associative)
                while (operatorStack.Any() && operatorStack.Peek().Precedence != 0 && 
                       operatorStack.Peek().Precedence >= token.Precedence)
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                } 
                operatorStack.Push(Operators.TryToOperator(token)); 
            }
        }

        while (operatorStack.Any())
        {
            if (Operators.IsParenthesis(operatorStack.Peek()))
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }
        }

        return outputQueue;
    }
}
