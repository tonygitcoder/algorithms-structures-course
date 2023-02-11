using calculator.Logic;

// Console.WriteLine("Enter the expression below:");
// ? means nullable, can receive null input
// string? userInput = Console.ReadLine();
const string userInput = "3 + 5/7 +6";

var output = Tokenizator.Tokenize(userInput);
Console.WriteLine(string.Join(", ", output));

var postfixOutput = PostfixConverter.ConvertToPostfix(output);
Console.WriteLine(string.Join(", ", postfixOutput));
