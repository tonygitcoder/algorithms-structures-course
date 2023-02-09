var operators = new List<char> { '+', '-', '*', '/'};

// const string consoleInput = "123 *(3+10)";
const string consoleInput = "5 *3+10";
var output = Tokenize(consoleInput);

Console.WriteLine(string.Join(", ", output));

List<string> Tokenize(string input)
{
    var numbersBuffer = new List<string>();
    var tokenizedInput = new List<string>();

    foreach (var character in input)
    {
        if (char.IsDigit(character))
        {
            numbersBuffer.Add(character.ToString());
            continue;
        }

        if (operators.Contains(character)) continue;
        if (numbersBuffer.Count > 0)
        {
            var token = string.Join("", numbersBuffer);
            tokenizedInput.Add(token);
        }

        numbersBuffer.Clear();
        tokenizedInput.Add(character.ToString());
    }

    return (tokenizedInput);
}