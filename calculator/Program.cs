using calculator.Logic;
using calculator.Misc;

// Console.WriteLine("Enter the expression below:");
// ? means nullable, can receive null input
// string? userInput = Console.ReadLine();
const string userInput = "3*(5 + 7)";

var output = Tokenizator.Tokenize(userInput);
Console.WriteLine(string.Join(", ", output));

var postfixOutput = PostfixConverter.ConvertToPostfix(output);
Console.WriteLine(string.Join(", ", postfixOutput));

// For test purposes only
Console.WriteLine(Operators.Evaluate(new MathUnit("3"), new MathUnit("5"), new Operator("+", 3)));