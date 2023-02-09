// var consoleInput = Console.ReadLine();
// Console.WriteLine(tokenize(input).ToString());

// var operators = new List<char> { '+', '-', '*', '/', ')', '(' };

using System;
using System.Collections.Generic;
using System.Linq;

var operators = new List<char> { '+', '-', '*', '/', '(', ')'};

// const string consoleInput = "123 *(3+10)";
const string consoleInput = "5 *(3+10)";
var output = Tokenize(consoleInput);
// var rpnInput = ConvertToRpn(output);

Console.WriteLine(string.Join(", ", output));
// Console.WriteLine(string.Join(", ", rpnInput));

// TODO: FIX Bug
List<string> Tokenize(string input)
{
    var numbersBuffer = new List<string>();
    var tokenizedInput = new List<string>();

    foreach (var character in input)
    {
        if (character == ' ')
        {
            continue;
        }
        
        if (char.IsDigit(character))
        {
            numbersBuffer.Add(character.ToString());
        }

        if (operators.Contains(character))
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

// TODO: Add parenthesis support
List<string> ConvertToRpn(List<string> tokens) {
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
            operatorStack.Push(token[0]);
        }
    }
    
    while(operatorStack.Count > 0) 
    {   
        outputQueue.Enqueue(operatorStack.Pop().ToString());
    }
    
    return outputQueue.ToList();
}
