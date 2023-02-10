namespace calculator;

public static class PostfixConverter
{
    // TODO: Add parenthesis support
    static Queue<string> ConvertToRPN(List<string> tokens) {
        var outputQueue = new Queue<string>();
        var operatorStack = new Stack<char>(); 

        foreach (var token in tokens)
        {
            var isNumeric = int.TryParse(token, out _);
            if (isNumeric)
            {
                outputQueue.Enqueue(token);
                continue;
            }       

            if (Operators.IsOperator(token[0]))
            {
                // while(operatorStack.Peek() != )
                operatorStack.Push(token[0]);
            }
        }
    
        while(operatorStack.Count > 0) 
        {   
            outputQueue.Enqueue(operatorStack.Pop().ToString());
        }
        
        return outputQueue;
    }
}