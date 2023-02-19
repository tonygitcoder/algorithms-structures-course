using hash;

var stringsDictionary = new StringsDictionary(5);
// stringsDictionary.Add("me", "18");
// Console.WriteLine(stringsDictionary.Get("me"));
// stringsDictionary.Remove("me");
// Console.WriteLine(stringsDictionary.Get("me"));


var lines = File.ReadAllLines("../../../dictionary.txt");
for (var i = 0; i < lines.Length;)
{
    var line = lines[i];
    var lineParts = line.Split(";", 2);
    var key = lineParts[0];
    var value = lineParts[1];
    stringsDictionary.Add(key, value);

    i++;
    // Console.WriteLine($"Caching Progress: {i * 100 / lines.Length}% ({i}/{lines.Length})");
}

while (true)
{
    Console.WriteLine("> Input a word you would like to have definition for:");
    var key = Console.ReadLine().ToUpper();

    try
    {
        var value = stringsDictionary.Get(key);
        Console.WriteLine($"> The definition is: {value}");
    }
    catch (KeyNotFoundException e)
    {
        Console.WriteLine(e.Message);
    }
}