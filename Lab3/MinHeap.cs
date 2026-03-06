using System;

namespace Lab3;

// public class MinHeap<T> where T : IComparable<T>
public class MinHeap<T> : Heap<T> where T : IComparable<T>
{
    public MinHeap(T[] initialArray = null) : base(initialArray){}


    public override T Extract()
    {
        return ExtractMin();
    }

    /// <summary>
    /// Removes and returns the max item in the min-heap.
    /// Time complexity: O( n )
    /// </summary>
    public override T ExtractMax()
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
    public override T ExtractMin()
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

    // TODO
    /// <summary>
    /// Updates the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public override void Update(T oldValue, T newValue)
    {
        // find the node to update - O(n)

        int? node_location = GetIndexOfValue(oldValue);


        if (node_location == null)
        {
            throw new InvalidOperationException(); 
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

        // case 1: the node has no children
        if (leftChildIndex >= Count && rightChildIndex >= Count)
        {
            return null;
        }

        // case 2: the node has two valid children
        if (leftChildIndex < Count && rightChildIndex < Count)
        {
            int smaller_index;
            // set smaller_index to the index of the smaller child
            if (array[leftChildIndex].CompareTo(array[rightChildIndex]) < 0) //left child smaller than right
            {
                smaller_index = leftChildIndex;
            }
            else
            {
                smaller_index = rightChildIndex;
            }

            //now that smaller_index is the smaller of the two children, compare do the parent
            if (array[smaller_index].CompareTo(array[index]) < 0) // smaller than parent
            {
                return smaller_index;
            }
            return null;
        }

        // case 3: only left is valid child
        if (leftChildIndex < Count && array[leftChildIndex].CompareTo(array[index]) < 0) //left child smaller than parent
        {
            return leftChildIndex;
        }

        // case 4: only right is valid child
        if (rightChildIndex < Count && array[rightChildIndex].CompareTo(array[index]) < 0) //right child exists and is smaller than parent
        {
            return rightChildIndex;
        }

        return null;

    }

    // Time Complexity: O( log n )
    protected override void TrickleUp(int index)
    {
        int follow_index = index;
        if (follow_index == 0 || follow_index > Count)
        {
            return;
        }

        //check if parent exists and is bigger
        while (array[follow_index].CompareTo(array[Parent(follow_index)]) < 0 && follow_index > 0)
        {

            Swap(follow_index, Parent(follow_index)); //swap parent and child values, set new index to swapped index
            follow_index = Parent(follow_index);
        }
    }

    // Time Complexity: O( log n )
    protected override void TrickleDown(int index)
    {
        int? smaller_child = GetSmallerOfChildren(index);

        if (smaller_child == null)
        {
            return;
        }

        Swap(index, (int)smaller_child); 
        TrickleDown((int)smaller_child);
    }
}