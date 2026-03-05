using System;

namespace Lab3;

public class MaxHeap<T> : Heap<T> where T : IComparable<T>
{

    public MaxHeap(T[] initialArray = null) : base(initialArray){}
    public override T Extract()
    {
        throw new NotImplementedException();
    }

    public override T ExtractMax()
    {
        throw new NotImplementedException();
    }

    public override T ExtractMin()
    {
        throw new NotImplementedException();
    }

    public override void Update(T oldValue, T newValue)
    {
        throw new NotImplementedException();
    }

    protected override void TrickleDown(int index)
    {
        throw new NotImplementedException();
    }

    protected override void TrickleUp(int index)
    {
        throw new NotImplementedException();
    }
}

