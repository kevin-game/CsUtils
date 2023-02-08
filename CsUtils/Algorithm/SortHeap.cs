/*
 * usage
using CsUtils.Algorithm;

MinHeap<int> heap = new MinHeap<int>();
var len = 100;
foreach (var _ in Enumerable.Range(0, len))  
{
    heap.Enqueue(RandomNumberGenerator.GetInt32(1000));
}

foreach (var _ in Enumerable.Range(0, len))  
{
    Console.WriteLine(heap.Dequeue());
}
 */

namespace CsUtils.Algorithm;

/// <summary>
/// 小顶堆
/// </summary>
/// <typeparam name="T"></typeparam>
public class MinHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = new();
    public int Count => _items.Count;

    public void Enqueue(T value)
    {
        _items.Add(value);
        SortFromLast();
    }

    /// <summary>
    /// 取出堆的最小值
    /// </summary>
    /// <returns></returns>
    public T Dequeue()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var result = _items[0];
        _items[0] = _items[Count - 1];
        _items.RemoveAt(Count - 1);

        SortFromFirst();
        return result;
    }

    public void Clear()
    {
        _items.Clear();
    }

    private void SortFromFirst()
    {
        var parent = 0;
        var leftChild = 1;
        while (leftChild < Count)
        {
            // 找到子节点中较小的那个
            int rightChild = leftChild + 1;
            int minChild = (rightChild < Count && _items[rightChild].CompareTo(_items[leftChild]) < 0)
                ? rightChild
                : leftChild;
            if (_items[minChild].CompareTo(_items[parent]) < 0)
            {
                // 如果子节点小于父节点, 交换子节点和父节点
                (_items[parent], _items[minChild]) = (_items[minChild], _items[parent]);
                parent = minChild;
                leftChild = (parent * 2) + 1;
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// 从后往前依次对各结点为根的子树进行筛选，使之成为堆，直到根结点
    /// </summary>
    /// <returns></returns>
    private void SortFromLast()
    {
        var child = Count - 1;
        while (child > 0)
        {
            int parent = (child - 1) / 2;
            //如果子节点小于父节点，交换子节点和父节点
            if (_items[child].CompareTo(_items[parent]) < 0)
            {
                (_items[child], _items[parent]) = (_items[parent], _items[child]);
            }
            else
            {
                break;
            }

            child = parent;
        }
    }
}

/// <summary>
/// 大顶堆
/// </summary>
/// <typeparam name="T"></typeparam>
public class MaxHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = new();
    public int Count => _items.Count;

    public void Enqueue(T value)
    {
        _items.Add(value);
        SortFromLast();
    }

    /// <summary>
    /// 取出堆的最小值
    /// </summary>
    /// <returns></returns>
    public T Dequeue()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var result = _items[0];
        _items[0] = _items[Count - 1];
        _items.RemoveAt(Count - 1);

        SortFromFirst();
        return result;
    }

    public void Clear()
    {
        _items.Clear();
    }

    private void SortFromFirst()
    {
        var parent = 0;
        var leftChild = 1;
        while (leftChild < Count)
        {
            // 找到子节点中较小的那个
            int rightChild = leftChild + 1;
            int minChild = (rightChild < Count && _items[rightChild].CompareTo(_items[leftChild]) > 0)
                ? rightChild
                : leftChild;
            if (_items[minChild].CompareTo(_items[parent]) > 0)
            {
                // 如果子节点小于父节点, 交换子节点和父节点
                (_items[parent], _items[minChild]) = (_items[minChild], _items[parent]);
                parent = minChild;
                leftChild = (parent * 2) + 1;
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// 从后往前依次对各结点为根的子树进行筛选，使之成为堆，直到根结点
    /// </summary>
    /// <returns></returns>
    private void SortFromLast()
    {
        var child = Count - 1;
        while (child > 0)
        {
            int parent = (child - 1) / 2;
            //如果子节点小于父节点，交换子节点和父节点
            if (_items[child].CompareTo(_items[parent]) > 0)
            {
                (_items[child], _items[parent]) = (_items[parent], _items[child]);
            }
            else
            {
                break;
            }

            child = parent;
        }
    }
}