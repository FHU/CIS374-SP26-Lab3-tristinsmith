using System;

namespace Lab3;

public class MinHeap<T> where T : IComparable<T>
{
    private T[] array;
    private const int initialSize = 8;

    public int Count { get; private set; }

    public int Capacity => array.Length;

    public bool IsEmpty => Count == 0;


    public MinHeap(T[] initialArray = null)
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

    /// <summary>
    /// Returns the min item but does NOT remove it.
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

    // TODO
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

    public T Extract()
    {
        return ExtractMin();
    }

    /// <summary>
    /// Removes and returns the max item in the min-heap.
    /// Time complexity: O( ? )
    /// </summary>
    public T ExtractMax()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        T current_max = default;
        for (int i = 0; i < Count; i++)
        {
            if (current_max.CompareTo(array[i]) < 0)
            {
                current_max = array[i];
            }
        }

        Remove(current_max);
        return current_max;
    }

    // TODO
    /// <summary>
    /// Removes and returns the min item in the min-heap.
    /// Time complexity: O( log(n) )
    /// </summary>
    public T ExtractMin()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        T minValue = array[0];
        Swap(0, Count - 1);
        Count--;
        TrickleDown(0);
        return minValue;
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

    // TODO
    /// <summary>
    /// Updates the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Update(T oldValue, T newValue)
    {
        // find the node to update - O(n)

        int? node_location = GetIndexOfValue(oldValue);

        if (node_location == null)
        {
            return;   
        }

        // update value - O(1)
        array[(int)node_location] = newValue;

        // trickle up or trickle down - O( log(n) )

        if (newValue.CompareTo(array[Parent((int)node_location)]) < 0) //updated node is less than parent
        {
            TrickleUp((int)node_location);
            return;
        }

        if (GetSmallerOfChildren((int)node_location) != null) //updated node has a smaller child
        {
            TrickleDown((int)node_location);
            return;
        }



    }

    /// <summary>
    /// Returns the index of the smallest immediate child at the given index
    /// </summary>
    private int? GetSmallerOfChildren(int index)
    {
        int leftChildIndex = LeftChild(index);
        int rightChildIndex = RightChild(index);

        if (leftChildIndex >= Count && rightChildIndex >= Count) //given node was a leaf
        {
            return null;
        }

        if (leftChildIndex < Count && rightChildIndex < Count) //given node has two children
        {
            int smaller_index = 0;
            // set smaller_index to the index of the smaller child
            if (array[leftChildIndex].CompareTo(array[rightChildIndex]) < 0) //left child smaller than right
            {
                smaller_index = leftChildIndex;
            }
            else
            {
                smaller_index = rightChildIndex;
            }

            if (array[smaller_index].CompareTo(array[index]) < 0) //smaller of the two children is smaller than parent
            {
                return smaller_index;
            }
            return null;
        }
        {
            
        }

        if (leftChildIndex < Count && array[leftChildIndex].CompareTo(array[index]) < 0) //left child exists and is smaller than parent
        {
            return leftChildIndex;
        }

        if (rightChildIndex < Count && array[rightChildIndex].CompareTo(array[index]) < 0) //right child exists and is smaller than parent
        {
            return rightChildIndex;
        }

        return null;

    }

    private int? GetIndexOfValue(T value)
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

    // TODO
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

    // TODO
    // Time Complexity: O( log n )
    private void TrickleUp(int index)
    {
        int follow_index = index;
        if (follow_index == 0)
        {
            return;
        }

        while (array[follow_index].CompareTo(array[Parent(follow_index)]) < 0) //current value less than parent value
        {
            Swap(follow_index, Parent(follow_index)); //swap parent and child values, set new index to swapped index
            follow_index = Parent(follow_index);
            
        }

    }

    // TODO
    // Time Complexity: O( log n )
    private void TrickleDown(int index)
    {
        int? smaller_child = GetSmallerOfChildren(index);

        if (smaller_child == null)
        {
            return;
        }

        Swap(index, (int)smaller_child); 
        TrickleDown((int)smaller_child);

    }

    // TODO
    /// <summary>
    /// Gives the position of a node's parent, the node's position in the heap.
    /// </summary>
    private static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    // TODO
    /// <summary>
    /// Returns the position of a node's left child, given the node's position.
    /// </summary>
    private static int LeftChild(int position)
    {
        return 2 * position + 1;
    }

    // TODO
    /// <summary>
    /// Returns the position of a node's right child, given the node's position.
    /// </summary>
    private static int RightChild(int position)
    {
        return 2 * position + 2;
    }

    private void Swap(int index1, int index2)
    {
        var temp = array[index1];

        array[index1] = array[index2];
        array[index2] = temp;
    }

    private void DoubleArrayCapacity()
    {
        Array.Resize(ref array, array.Length * 2);
    }
}