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

    var computedInput 
        = Computation.ComputeOutput(postfixInput);
    OutputResult(computedInput);
}

void OutputResult(IEnumerable<MathUnit> result)
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine(string.Join(", ", result));
    Console.ResetColor();
}