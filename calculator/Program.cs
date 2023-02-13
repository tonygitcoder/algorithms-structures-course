using calculator.Logic;
using calculator.Misc;

while (true)
{
    Console.WriteLine("Enter the expression below. Type 'exit' to exit.");

    var userInput = Console.ReadLine();
    if (userInput == "exit") break;
    
    // TODO: input syntaxys validation
    var tokenizedInput 
        = Tokenizator.Tokenize(userInput);
    OutputResult(tokenizedInput);

    var postfixInput 
        = PostfixConverter.ConvertToPostfix(tokenizedInput);
    OutputResult(postfixInput);
    
    var computedInput = new Stack<MathUnit>();
    try
    {
        computedInput 
            = Computation.ComputeOutput(postfixInput);
    }
    catch (DivideByZeroException e)
    {
        OutputColor("Error: " + e.Message, ConsoleColor.Red);
    }
    
    OutputResult(computedInput);
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