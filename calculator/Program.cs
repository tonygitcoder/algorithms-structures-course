using calculator;

// var consoleInput = Console.ReadLine();
const string consoleInput = "1 + 2 * 3 - 4";

var output = Tokenize(consoleInput);
Console.WriteLine(string.Join(", ", output));

// var rpnInput = ConvertToRpn(output);
// Console.WriteLine(string.Join(", ", rpnInput));


List<string> Tokenize(string input)
{
    var numbersBuffer = new List<string>();
    var tokenizedInput = new List<string>();

    foreach (var character in input)
    {
        if (char.IsDigit(character))
        {
            numbersBuffer.Add(character.ToString());
        }
        
        
        if (Operators.IsOperator(character))
        {
            if (numbersBuffer.Count > 0)
            {
                var token = string.Join("", numbersBuffer);
                tokenizedInput.Add(token);
            }
            
            numbersBuffer.Clear();
            tokenizedInput.Add(character.ToString());   
        }
    }
    
    if (numbersBuffer.Count > 0)
    {
        var token = string.Join("", numbersBuffer);
        tokenizedInput.Add(token);
    }

    return (tokenizedInput);
}

/*
// TODO: Add parenthesis support
Queue<string> ConvertToRpn(List<string> tokens) {
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

        if (operators.Contains(token[0]))
        {
            while(operatorStack.Peek() != )
            operatorStack.Push(token[0]);
        }
    }
    
    while(operatorStack.Count > 0) 
    {   
        outputQueue.Enqueue(operatorStack.Pop().ToString());
    }
    
    return outputQueue;
}
*/