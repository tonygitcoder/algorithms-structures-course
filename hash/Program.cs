using hash;

var stringsDictionary = new StringsDictionary(16000);
// stringsDictionary.Add("me", "18");
// Console.WriteLine(stringsDictionary.Get("me"));
// stringsDictionary.Remove("me");
// Console.WriteLine(stringsDictionary.Get("me"));


var lines = File.ReadAllLines("../../../dictionary.txt");
for (var i = 0; i < lines.Length;)
{
    var lineParts = lines[i].Split(";", 2);
    stringsDictionary.Add(lineParts[0], lineParts[1]);

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