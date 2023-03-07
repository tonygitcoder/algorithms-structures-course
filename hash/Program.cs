using hash;

// intersection between two arrays
var array1 = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
var array2 = new[] {1, 3, 5, 7, 9, 11, 13, 15, 17, 19};

var output = new List<int>();

var dict = array1.ToDictionary(element => element);
foreach (var element in array2)
{
    if (dict.TryGetValue(element, out _)) output.Add(element);
}

Console.WriteLine(string.Join(", ", output));

// var stringsDictionary = new StringsDictionary();
//
// var lines = File.ReadAllLines("../../../dictionary.txt");
// for (var i = 0; i < lines.Length;)
// {
//     var lineParts = lines[i].Split(";", 2);
//     stringsDictionary.Add(lineParts[0], lineParts[1]);
//
//     i++;
//     // Console.WriteLine($"Caching Progress: {i * 100 / lines.Length}% ({i}/{lines.Length})");
// }
//
// while (true)
// {
//     Console.WriteLine("> Input a word you would like to have definition for:");
//     var key = Console.ReadLine().ToUpper();
//
//     try
//     {
//         var value = stringsDictionary.Get(key);
//         Console.WriteLine($"> The definition is: {value}");
//     }
//     catch (KeyNotFoundException e)
//     {
//         Console.WriteLine(e.Message);
//     }
// }