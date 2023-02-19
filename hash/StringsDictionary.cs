namespace hash;

public class StringsDictionary
{
    private readonly LinkedList[] _buckets;

    public StringsDictionary(int size)
    {
        _buckets = new LinkedList[size];
    }
        
    public void Add(string key, string value)
    {
        var bucketIndex = CalculateHash(key);

        var bucket = _buckets[bucketIndex];
        if (bucket == default(LinkedList))
        {
            _buckets[bucketIndex] = new LinkedList();
            bucket = _buckets[bucketIndex];
        }

        var keyValue = new KeyValuePair(key, value);
        
        bucket.Add(keyValue);
    }

    public void Remove(string key)
    {
        var bucketIndex = CalculateHash(key);
        
        var bucket = _buckets[bucketIndex] ?? throw new DirectoryNotFoundException();
        bucket.RemoveByKey(key);
    }

    public string Get(string key)
    {
        var bucketIndex = CalculateHash(key);

        var bucket = _buckets[bucketIndex] ?? throw new DirectoryNotFoundException();
        try
        {
            var value = bucket.GetItemWithKey(key);
            if (value == null) throw new KeyNotFoundException();
            return value.Value;
        }
        catch (KeyNotFoundException)
        {
            throw new KeyNotFoundException();
        }
    }

    private int CalculateHash(string key)   
    {
        var hash = 0;
        for (var i = 0; i < key.Length; i++)
        {
            int charNum = key[i];
            var nonZeroIndex = i + 1;
            
            // To reduce collisions, otherwise abc = acb
            hash += charNum * nonZeroIndex;
        }

        return hash % _buckets.Length;
    }
}