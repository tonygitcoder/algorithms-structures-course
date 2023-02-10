using calculator.Logic;

// Console.WriteLine("Enter the expression below:");
// ? means nullable, can receive null input
// string? userInput = Console.ReadLine();
const string userInput = "1 + 2 * 3 - 4";

var output = Tokenizator.Tokenize(userInput);
Console.WriteLine(string.Join(", ", output));

// Console.WriteLine(string.Join(", ", rpnInput));
