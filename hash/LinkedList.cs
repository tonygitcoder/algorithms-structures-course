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
        if (_last != null)
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
        do
        {
            if (currentNode.Next.Pair.Key == key)
            {
                currentNode.Next = null;
            }

            currentNode = currentNode.Next;
        } while (currentNode.Next != null);
    }

    public KeyValuePair GetItemWithKey(string key)
    {
        var currentNode = _first;
        do
        {   
            if (currentNode.Next.Pair.Key == key)
            {
                return currentNode.Next.Pair;
            }

            currentNode = currentNode.Next;
        } while (currentNode.Next != null);

        return null;
    }
}