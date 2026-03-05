using System;

namespace Lab3;

public class MaxHeap<T> : Heap<T> where T : IComparable<T>
{

    public MaxHeap(T[] initialArray = null) : base(initialArray) { }
    public override T Extract()
    {
        return ExtractMax();
    }

    public override T ExtractMax()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        T maxValue = array[0];
        Swap(0, Count - 1);
        Count--;
        TrickleDown(0);
        return maxValue;
    }

    public override T ExtractMin()
    {
        System.Console.WriteLine("Calling extract min");
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        T current_min = default;
        for (int i = 0; i < Count; i++)
        {
            //the contains check if sof an edge case where the default may be 0 when 0 isn't in the heap
            if (current_min.CompareTo(array[i]) > 0 || !Contains(current_min))
            {
                current_min = array[i];
            }
        }

        Remove(current_min);
        return current_min;
    }

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

        if (newValue.CompareTo(array[Parent((int)node_location)]) > 0) //updated node is greater than parent
        {
            TrickleUp((int)node_location);
            return;
        }

        if (GetLargerOfChildren((int)node_location) != null) //updated node has a larger child
        {
            TrickleDown((int)node_location);
            return;
        }
    }

    protected override void TrickleDown(int index)
    {
        int? larger_child = GetLargerOfChildren(index);

        if (larger_child == null)
        {
            return;
        }

        Swap(index, (int)larger_child); 
        TrickleDown((int)larger_child);
    }

    protected override void TrickleUp(int index)
    {
        int follow_index = index;
        if (follow_index == 0 || follow_index > Count)
        {
            return;
        }

        //check if parent exists and is smaller
        while (array[follow_index].CompareTo(array[Parent(follow_index)]) > 0 && follow_index > 0)
        {

            Swap(follow_index, Parent(follow_index)); //swap parent and child values, set new index to swapped index
            follow_index = Parent(follow_index);
        }
    }

    private int? GetLargerOfChildren(int index)
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
            int larger_index;
            // set smaller_index to the index of the smaller child
            if (array[leftChildIndex].CompareTo(array[rightChildIndex]) > 0) //left child larger than right
            {
                larger_index = leftChildIndex;
            }
            else
            {
                larger_index = rightChildIndex;
            }

            //now that smaller_index is the smaller of the two children, compare do the parent
            if (array[larger_index].CompareTo(array[index]) > 0) // larger than parent
            {
                return larger_index;
            }
            return null;
        }

        // case 3: only left is valid child
        if (leftChildIndex < Count && array[leftChildIndex].CompareTo(array[index]) > 0) //left child larger than parent
        {
            return leftChildIndex;
        }

        // case 4: only right is valid child
        if (rightChildIndex < Count && array[rightChildIndex].CompareTo(array[index]) > 0) //right child exists and is larger than parent
        {
            return rightChildIndex;
        }

        return null;

    }
}

