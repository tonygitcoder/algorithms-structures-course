using System.Collections;

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
        
    public LinkedListNode? Next { get; set; }

    public LinkedListNode(KeyValuePair pair, LinkedListNode? next = null)
    {
        Pair = pair;
        Next = next;
    }
}

public class LinkedList : IEnumerable
{
    private LinkedListNode? _first;
    private LinkedListNode? _last;
    
    public void Add(KeyValuePair pair)
    {
        if (GetItemWithKey(pair.Key) != null)
            throw new KeyAlreadyExistsException(pair.Key);
        
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
        var previousNode = _first;
        
        do
        {
            if (currentNode?.Pair.Key == key)
            {
                if (previousNode == _first)
                {
                    _first = default;
                }
                else
                {
                    if (previousNode != null) previousNode.Next = default;
                }
                return;
            }
        
            currentNode = currentNode?.Next;
        } while (currentNode != default);

        throw new KeyNotFoundException();
    }

    public KeyValuePair? GetItemWithKey(string key)
    {
        var currentNode = _first;
        if (currentNode == null) return null;
        do
        {
            if (currentNode.Pair.Key == key)
            {
                return currentNode.Pair;
            }

            currentNode = currentNode.Next;
        } while (currentNode != null);

        return null;
    }

    public IEnumerator GetEnumerator()
    {
        var currentNode = _first;
        if (currentNode == null) throw new NullReferenceException();
        do
        {
            yield return currentNode.Pair; 
            
            currentNode = currentNode.Next;
        } while (currentNode?.Next != null);
    }
}

public class KeyAlreadyExistsException : Exception
{
    public KeyAlreadyExistsException(string key) : base($"Key {key} already exists")
    {
    }
}