namespace hash;

public class StringsDictionary
{
    private const int InitialSize = 10;

    private LinkedList[] _buckets = new LinkedList[InitialSize];
        
    public void Add(string key, string value)
    {
        var hash = CalculateHash(key);
        var bucketIndex = hash % _buckets.Length;
        
        Console.WriteLine(hash);
        Console.WriteLine(bucketIndex);
        
        var bucket = _buckets[bucketIndex];
        if (Equals(bucket, default(LinkedList)))
        {
            _buckets[bucketIndex] = new LinkedList();
            bucket = _buckets[bucketIndex];
        }

        var keyValue = new KeyValuePair(key, value);
        
        bucket.Add(keyValue);
    }

    public void Remove(string key)
    {
        var hash = CalculateHash(key);
        var bucketIndex = hash % _buckets.Length;
        
        var bucket = _buckets[bucketIndex] ?? throw new KeyNotFoundException();
        bucket.RemoveByKey(key);
    }

    public string Get(string key)
    {
        var hash = CalculateHash(key);
        var bucketIndex = hash % _buckets.Length;
        
        Console.WriteLine(hash);
        Console.WriteLine(bucketIndex);

        var bucket = _buckets[bucketIndex] ?? throw new KeyNotFoundException();
        return bucket.GetItemWithKey(key).Value;
    }

    private int CalculateHash(string key)
    {
        var hash = 0;
        for (var i = 0; i < key.Length; i++)
        {
            int charNum = key[i];
            var nonZeroIndex = i + 1;
            
            // To reduce collisions, otherwise abc = acb
            hash += charNum * nonZeroIndex + nonZeroIndex;
        }

        return hash;
    }
}