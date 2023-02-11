namespace calculator.Logic;

using Misc;

public static class Tokenizator
{
    // tokenizing function
    public static List<MathUnit> Tokenize(string input)
    {
        var numberBuffer = new List<MathUnit>();
        var output = new List<MathUnit>();
        
        // looping through each symbol
        foreach (var symbol in input) 
        {
            var unit = new MathUnit(symbol.ToString());
            
            // if its a number, we add it to buffer
            if (Operators.IsInt(unit) || Operators.IsFloatDelimiter(unit)) 
            {
                numberBuffer.Add(unit);
            }
            
            // if it's an operator and the buffer has numbers, we tokenize them
            if (!(Operators.IsOperatorish(unit))) continue;
            if (numberBuffer.Any())
            {
                output.Add(TokenizeBuffer(numberBuffer));
            }
                
            numberBuffer.Clear();
            //also adding the operator as token
            output.Add(unit);
        }
        
        // if the buffer still has smth, we add it to tokenized
        if (numberBuffer.Any()) 
        {
            output.Add(TokenizeBuffer(numberBuffer));
        }
        return output;
    }

    private static MathUnit TokenizeBuffer(List<MathUnit> buffer)
    {
        return new MathUnit(string.Join("", buffer));
    }
}