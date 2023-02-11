using calculator.Logic;
using calculator.Misc;

while (true)
{
    Console.WriteLine("Enter the expression below. Type 'exit' to exit.");

    var userInput = Console.ReadLine() ?? "";
    if (userInput == "exit") break;
    
    var tokenizedInput 
        = Tokenizator.Tokenize(userInput);
    OutputResult(tokenizedInput);

    var postfixInput 
        = PostfixConverter.ConvertToPostfix(tokenizedInput);
    OutputResult(postfixInput);
    
    try
    {
        var computedInput 
            = Computation.ComputeOutput(postfixInput);
        OutputResult(computedInput);
    }
    catch (Exception e)
    {
        OutputColor("Error: " + e.Message, ConsoleColor.Red);
    }
}

void OutputResult(IEnumerable<MathUnit> result)
{
    OutputColor(string.Join(", ", result),
        ConsoleColor.DarkGreen);
}

void OutputColor(string text, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(text);
    Console.ResetColor();
}