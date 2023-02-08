using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsUtils.Algorithm;

/*
 * demo
 int[] randomIntArray = { 1,2,3,9,8,7,6,5,4};
Heap<int> heap = new(randomIntArray);
heap.Sort(AscComparison);
PrintAarry(randomIntArray);



int count = 0;

static int AscComparison(int x, int y)
{
    if (x > y)
    {
        return 1;
    }
    else if (x == y)
    {
        return 0;
    }
    else
    {
        return -1;
    }
}

void PrintAarry(int[] array)
{
    int level = 0;
    for (int i = 0; i < array.Length; )
    {
        for (int j = 0; j < Math.Pow(2, level) && i < array.Length; j++, i++)
        {
            Console.Write($"{array[i]} ");
        }

        level++;
        Console.WriteLine();
    }
}
 */

public class Heap<T>
{
    #region Fields

    private int _heapSize;
    private T[] _array;

    #endregion

    #region Properties

    public int HeapSize
    {
        get { return _heapSize; }
        set { _heapSize = value; }
    }

    #endregion

    #region Constructors

    public Heap(T[] array, int heapSize)//TODO:没啥用，可以考虑删掉
    {
        if (heapSize > array.Length)
        {
            throw new Exception("The heap size is larger than the array length");
        }

        _array = array;
        _heapSize = heapSize;
    }

    public Heap(T[] array)
    {
        _array = array;
        _heapSize = array.Length;
    }

    #endregion

    #region Methods

    int Parrent(int index)
    {
        return (index - 1) / 2;
    }

    int LeftChild(int index)
    {
        return 2 * index + 1;
    }

    int RightChild(int index)
    {
        return 2 * index + 2;
    }

    void MHeapify(int rootIndex, Comparison<T> comparison)
    {
        int leftChildIndex = LeftChild(rootIndex);
        int rightChildIndex = RightChild(rootIndex);

        int extremumIndex = rootIndex;
        if (leftChildIndex < _heapSize && comparison(_array[leftChildIndex], _array[rootIndex]) > 0)
        {
            extremumIndex = leftChildIndex;
        }

        if (rightChildIndex < _heapSize && comparison(_array[rightChildIndex], _array[extremumIndex]) > 0)
        {
            extremumIndex = rightChildIndex;
        }

        if (extremumIndex != rootIndex)
        {
            HeapHelper.Exchange<T>(ref _array[extremumIndex], ref _array[rootIndex]);
            MHeapify(extremumIndex, comparison);
        }
    }

    private void BuildMHeap(Comparison<T> comparison)
    {
        for (int i = _array.Length / 2 - 1; i >= 0; i--)
        {
            MHeapify(i, comparison);
        }
    }

    public void Sort(Comparison<T> comparison)
    {
        BuildMHeap(comparison);
        for (int i = _array.Length - 1; i > 0; i--)
        {
            HeapHelper.Exchange(ref _array[i], ref _array[0]);
            _heapSize--;
            MHeapify(0, comparison);
        }
    }

    #endregion
}

public class HeapHelper
{
    public static void Exchange<T>(ref T x, ref T y)
    {
        T temp = x;
        x = y;
        y = temp;
    }
}


