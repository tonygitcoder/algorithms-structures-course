using System;
using System.Collections.Generic;
using System.Linq;

// main code
var operators = new List<char> { '+', '-', '*', '/', '(', ')'};
Console.WriteLine("Enter the expression below:");
string userInput = Console.ReadLine(); //"10 * (7-5) + 1"
var output = Tokenize(userInput);
Console.WriteLine(string.Join(", ", output));

// tokenizing function
List<string> Tokenize(string input)
{
    List<string> nBuffer = new List<string>();
    List<string> tokenized = new List<string>();
    foreach (char symbol in input) // looping through each symbol
    {
        if (char.IsDigit(symbol)) // if its a number, we add it to buffer
        {
            nBuffer.Add(symbol.ToString());
        }

        if (operators.Contains(symbol)) // if it's an operator and the buffer has numbers, we tokenize them
        {
            if (nBuffer.Any())
            {
                string token = string.Join("", nBuffer);
                tokenized.Add(token);
            }
            nBuffer.Clear();
            tokenized.Add(symbol.ToString()); //also adding the operator as token
        }
    }
    if (nBuffer.Any()) // if the buffer still has smth, we add it to tokenized
    {
        string token = String.Join("", nBuffer);
        tokenized.Add(token);
    }
    return tokenized;
}