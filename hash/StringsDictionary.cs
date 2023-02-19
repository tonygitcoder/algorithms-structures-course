namespace hash;

public class StringsDictionary
{
    private const float LoadFactorThreshold = 0.75f;
    private LinkedList[] _buckets;
    private int _size;
    private int _capacity;
    private float LoadFactor => (float)_size / _capacity;

    public StringsDictionary(int capacity = 5)
    {
        _capacity = capacity;
        _buckets = new LinkedList[_capacity];
    }
        
    public void Add(string key, string value)
    {
        if (LoadFactor >= LoadFactorThreshold) Rebalance();

        var bucketIndex = CalculateHash(key);
        var bucket = _buckets[bucketIndex];
        
        if (bucket == null)
        {
            _buckets[bucketIndex] = new LinkedList();
            bucket = _buckets[bucketIndex];
        }

        var keyValue = new KeyValuePair(key, value);
        
        if (bucket.GetItemWithKey(key) != null)
        {
            Console.WriteLine($"Key {key} already exists. Replacing value with {value}");
            bucket.RemoveByKey(key);
        }

        bucket.Add(keyValue);
        _size++;
    }

    private void Rebalance()
    {
        Console.WriteLine($"_size: {_size}, _capacity: {_capacity}, loadFactor: {LoadFactor}");

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

    public void Remove(string key)
    {
        var bucketIndex = CalculateHash(key);
        
        var bucket = _buckets[bucketIndex];
        bucket.RemoveByKey(key);
        _size--;
    }

    public string Get(string key)
    {
        var bucketIndex = CalculateHash(key);
        var bucket = _buckets[bucketIndex];
        
        var pair = bucket.GetItemWithKey(key) ?? throw new KeyNotFoundException();
        
        return pair.Value;
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