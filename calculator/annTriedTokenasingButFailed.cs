//using System;
//using System.Collections.Generic;
//using System.Linq;

/* string[] operators = { "*", "/", "-", "+", ")", "("};    
List<string> authorsRange = new List<string>(operators);

List<string> numberBuffer = new List<string>();  
List<string> tokenised = new List<string>();
string input = "2 + 10 * (3 - 1)";

foreach (char symbol in input){
    if (char.IsDigit(symbol))
    {
        string s = char.ToString(symbol);
        numberBuffer.Add(s);
    }
    else if (operators.Contains(char.ToString(symbol)))
    {
        if (numberBuffer.Count > 0){
            string token = "";
            foreach(var number in numberBuffer){
                token += number;
            }
            numberBuffer.Clear();
            tokenised.Add( char.ToString(symbol));
        }
    }
}
Console.WriteLine(tokenised);*/