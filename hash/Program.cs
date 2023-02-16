using hash;

var stringsDictionary = new StringsDictionary();
stringsDictionary.Add("me", "18");
Console.WriteLine(stringsDictionary.Get("me"));
stringsDictionary.Remove("me");
Console.WriteLine(stringsDictionary.Get("me"));