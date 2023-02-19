namespace hash;

public class StringsDictionary
{
    private const float LoadFactorThreshold = 0.75f;
    private LinkedList[] _buckets;
    private int _size = 0;
    private int _capacity;

    public StringsDictionary(int capacity = 5)
    {
        _capacity = capacity;
        _buckets = new LinkedList[_capacity];
    }
        
    public void Add(string key, string value)
    {
        var loadFactor = (float)_size / _capacity;
        if (loadFactor >= LoadFactorThreshold)
        {
            Console.WriteLine($"_size: {_size}, _capacity: {_capacity}, loadFactor: {loadFactor}");
            
            _capacity *= 2;
            var newBuckets = new LinkedList[_capacity];
            foreach (var element in _buckets)
            {
                if (element == null) continue;
                foreach (KeyValuePair pair in element)
                {
                    var newBucketIndex = CalculateHash(pair.Key);
                    if (newBuckets[newBucketIndex] == null)
                    {
                        newBuckets[newBucketIndex] = new LinkedList();
                    }
                    newBuckets[newBucketIndex].Add(pair);
                }
            }
            _buckets = newBuckets;
        }
        
        var bucketIndex = CalculateHash(key);

        var bucket = _buckets[bucketIndex];
        if (bucket == null)
        {
            _buckets[bucketIndex] = new LinkedList();
            bucket = _buckets[bucketIndex];
        }

        var keyValue = new KeyValuePair(key, value);
        
        bucket.Add(keyValue);
        _size++;
    }

    public void Remove(string key)
    {
        var bucketIndex = CalculateHash(key);
        
        var bucket = _buckets[bucketIndex] ?? throw new DirectoryNotFoundException();
        bucket.RemoveByKey(key);
        _size--;
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