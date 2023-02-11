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
            if (Operators.IsFloat(token))
            {
                outputQueue.Enqueue(token);
            }
            else if (Operators.IsOperator(token)) 
            {
                // there is an operator o2 at the top of the operator stack
                // which is not a left parenthesis, 
                // and (o2 has greater precedence than o1
                // or (o1 and o2 have the same precedence and o1 is left-associative)

                var tokenOperator = Operators.TryToOperator(token);
                while (operatorStack.Any() && Operators.IsOperator(operatorStack.Peek()) && 
                       operatorStack.Peek().Precedence >= tokenOperator.Precedence)
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                } 
                operatorStack.Push(Operators.TryToOperator(token)); 
            }
            else if (Operators.IsLeftParenthesis(token))
            {
                operatorStack.Push(token);
            }
        }

        while (operatorStack.Any())
        {
            if (!Operators.IsLeftParenthesis(operatorStack.Peek()))
            {
                var parenthesis = operatorStack.Pop();
                outputQueue.Enqueue(parenthesis);
            }
        }

        return outputQueue;
    }
}
