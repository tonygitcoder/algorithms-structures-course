using calculator.Misc;

namespace calculator.Logic;

public static class Tokenizator
{
    // tokenizing function
    public static List<string> Tokenize(string input)
    {
        List<string> nBuffer = new List<string>();
        List<string> tokenized = new List<string>();
        
        // looping through each symbol
        foreach (char symbol in input) 
        {
            // if its a number, we add it to buffer
            if (char.IsDigit(symbol)) 
            {
                nBuffer.Add(symbol.ToString());
            }
            
            // if it's an operator and the buffer has numbers, we tokenize them
            if (Operators.IsOperator(symbol)) 
            {
                if (nBuffer.Any())
                {
                    string token = string.Join("", nBuffer);
                    tokenized.Add(token);
                }
                
                nBuffer.Clear();
                //also adding the operator as token
                tokenized.Add(symbol.ToString()); 
            }
        }
        
        // if the buffer still has smth, we add it to tokenized
        if (nBuffer.Any()) 
        {
            string token = String.Join("", nBuffer);
            tokenized.Add(token);
        }
        return tokenized;
    }
}