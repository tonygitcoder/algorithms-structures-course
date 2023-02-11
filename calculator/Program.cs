using calculator.Logic;
using calculator.Misc;

// Console.WriteLine("Enter the expression below:");
// ? means nullable, can receive null input
// string? userInput = Console.ReadLine();
const string userInput = "1/3*6+(5-3)";

var tokenOutput = Tokenizator.Tokenize(userInput);
Console.WriteLine(string.Join(", ", tokenOutput));

var postfixOutput = PostfixConverter.ConvertToPostfix(tokenOutput);
Console.WriteLine(string.Join(", ", postfixOutput));

var finalOutput = Computation.ComputeOutput(postfixOutput);
Console.WriteLine(string.Join(", ", finalOutput));

// For test purposes only
//Console.WriteLine(Operators.Evaluate(new MathUnit("3"), new MathUnit("5"), new Operator("+", 3)));