using calculator.Misc;

namespace calculator.Logic;

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

/* Ann's version
// main code
var operators = new List<char> { '+', '-', '*', '/', '(', ')', '^'};
// Console.WriteLine("Enter the expression below:");
string userInput = "3 +5 /7 +6";
var tokenOutput = Tokenize(userInput);
var output = string.Join(" ", tokenOutput);
Console.WriteLine(output);

var reverseOutput = ConvertToPostfix(tokenOutput);
Console.WriteLine(string.Join(" ", reverseOutput));

// reverse polish notation
Queue<string> ConvertToPostfix(List<string> input)
{
    // introducing stack and queue
    var operatorStack = new Stack<char>();
    var outputQueue = new Queue<string>();
    foreach (string token in input) //loping through each token
    {
        char t = char.Parse(token);
        if (char.IsDigit(t))
        {
            outputQueue.Enqueue(token);
        }

        else if (operators.Contains(t)) //for operators
        {
            // there is an operator o2 at the top of the operator stack which is not a left parenthesis, 
            //and (o2 has greater precedence than o1 or (o1 and o2 have the same precedence and o1 is left-associative)
            while (operatorStack.Any() && CheckPriority(operatorStack.Peek()) != 0 && 
                   CheckPriority(operatorStack.Peek()) >= CheckPriority(t))
            {
                var element = char.ToString(operatorStack.Pop());
                outputQueue.Enqueue(element);
            } 
            operatorStack.Push(t); 
        }
    }

    while (operatorStack.Any())
    {
        if (CheckPriority(operatorStack.Peek()) != 0)
        {
            var token = char.ToString(operatorStack.Pop());
            outputQueue.Enqueue(token);
        }
    }
    Console.WriteLine(string.Join(" ",operatorStack));

    return outputQueue;
}

// check priority function

int CheckPriority(char operation)
{
    if (char.ToString(operation) == "+" || char.ToString(operation) == "-")
    {
        return 1;
    }
    else if (char.ToString(operation) == "*" || char.ToString(operation) == "/")
    {
        return 2;
    }
    else if (char.ToString(operation) == "^")
    {
        return 3;
    }
    else return 0; // if () return 0
}

// tokenizing function
List<string> Tokenize(string input)
{
    List<string> nBuffer = new List<string>();
    List<string> tokenized = new List<string>();
    foreach (char s in input) // looping through each symbol
    {
        if (char.IsDigit(s)) // if its a number, we add it to buffer
        {
            nBuffer.Add(s.ToString());
        }

        if (operators.Contains(s)) // if it's an operator and the buffer has numbers, we tokenize them
        {
            if (nBuffer.Any())
            {
                string token = string.Join("", nBuffer);
                tokenized.Add(token);
            }
            nBuffer.Clear();
            tokenized.Add(s.ToString()); //also adding the operator as token
        }
    }
    if (nBuffer.Any()) // if the buffer still has smth, we add it to tokenized
    {
       string token = String.Join("", nBuffer);
       tokenized.Add(token);
    }
    return tokenized;
}*/