namespace hash;

public class KeyValuePair
{
    public string Key { get; }

    public string Value { get; }

    public KeyValuePair(string key, string value)
    {
        Key = key;
        Value = value;
    }
}

public class LinkedListNode
{
    public KeyValuePair Pair { get; }
        
    public LinkedListNode Next { get; set; }

    public LinkedListNode(KeyValuePair pair, LinkedListNode next = null)
    {
        Pair = pair;
        Next = next;
    }
}

public class LinkedList
{
    private LinkedListNode _first;
    private LinkedListNode _last;
    
    public void Add(KeyValuePair pair)
    {
        if (!Equals(_last, default(LinkedListNode)))
        {
            _last.Next = new LinkedListNode(pair);
            _last = _last.Next;
        }
        else
        {
            _first = new LinkedListNode(pair);
            _last = _first;
        }
    }

    public void RemoveByKey(string key)
    {
        var currentNode = _first;
        var previousNode = _first;
        
        do
        {
            if (currentNode.Pair.Key == key)
            {
                if (previousNode == _first)
                {
                    _first = default;
                }
                else
                {
                    previousNode.Next = default;  
                }
                return;
            }
        
            currentNode = currentNode.Next;
        } while (currentNode != default);

        throw new KeyNotFoundException();
    }

    public KeyValuePair GetItemWithKey(string key)
    {
        var currentNode = _first;
        if (Equals(currentNode, default(LinkedListNode))) throw new KeyNotFoundException();
        do
        {
            if (currentNode != null && currentNode.Pair.Key == key)
            {
                return currentNode.Pair;
            }

            if (currentNode != null) currentNode = currentNode.Next;
        } while (Equals(currentNode?.Next, default(LinkedListNode)));

        return null;
    }
}