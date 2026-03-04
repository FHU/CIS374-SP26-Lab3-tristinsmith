using System;

namespace Lab3;

public abstract class Heap<T> where T : IComparable<T>
{
    protected T[] array;
    protected const int initialSize = 8;
    public int Count { get; protected set; }
    public int Capacity => array.Length;
    public bool IsEmpty => Count == 0;

    public Heap(T[] initialArray = null)
    {
        array = new T[initialSize];

        if (initialArray == null)
        {
            return;
        }

        foreach (var item in initialArray)
        {
            Add(item);
        }
    }

    protected abstract void TrickleDown(int index);
    protected abstract void TrickleUp(int index);

    public abstract T Extract();
    public abstract T ExtractMax();
    public abstract T ExtractMin();
    public abstract void Update(T oldValue, T newValue);
    

    /// <summary>
    /// Adds given item to the heap.
    /// Time complexity: O(log(n)) ***BUT*** it might be O(N) if we have to resize
    /// </summary>
    public void Add(T item)
    {
        array[Count] = item;
        Count++;

        TrickleUp(Count);

        if (Count == Capacity)
        {
            DoubleArrayCapacity();
        }

    }

    /// <summary>
    /// Removes the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Remove(T value)
    {
        // find the node to remove
        int? index_to_remove = GetIndexOfValue(value);

        if (index_to_remove == null)
        {
            throw new InvalidOperationException();
        }

        // swap with last
        Swap(Count, (int)index_to_remove);

        // trickleX
        TrickleDown((int)index_to_remove); //trickle down last value now at removed index

        Count--;

    }

    /// <summary>
    /// Returns the index of the first node with value
    /// </summary>
    protected int? GetIndexOfValue(T value)
    {

        for (int i = 0; i < Count; i++)
        {
            if (array[i].CompareTo(value) == 0)
            {
                return i;
            }
        }

        return null;
    }




    /// <summary>
    /// Returns the root item but does NOT remove it.
    /// Time complexity: O( 1 )
    /// </summary>
    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }
        return array[0];
    }


    /// <summary>
    /// Returns true if the heap contains the given value; otherwise false.
    /// Time complexity: O( n )
    /// </summary>
    public bool Contains(T value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (array[i].CompareTo(value) == 0)
            {
                return true;
            }
        }
        return false;
    }

    protected static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    protected static int LeftChild(int position)
    {
        return 2 * position + 1;
    }

    // TODO
    /// <summary>
    /// Returns the position of a node's right child, given the node's position.
    /// </summary>
    protected static int RightChild(int position)
    {
        return 2 * position + 2;
    }

    protected void Swap(int index1, int index2)
    {
        var temp = array[index1];

        array[index1] = array[index2];
        array[index2] = temp;
    }

    protected void DoubleArrayCapacity()
    {
        Array.Resize(ref array, array.Length * 2);
    }
}