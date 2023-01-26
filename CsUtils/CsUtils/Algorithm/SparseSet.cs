using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * fast insert and find in O(1) speed
 * only support unsigned int
 */

namespace CsUtils.Algorithm;

public class SparseSet
{
    private readonly uint _max;
    private uint _n = 0;
    private bool disposedValue;
    private readonly uint[] _dense;
    private readonly uint[] _sparse;

    public SparseSet(uint maxValue = uint.MaxValue-1)
    {
        _max = maxValue == uint.MaxValue ? uint.MaxValue-1 : maxValue;
        _dense = new uint[_max];
        _sparse = new uint[_max];
    }

    public void Add(uint value)
    {
        if (value <= _max && !Contains(value))
        {
            _dense[_n] = value;
            _sparse[value] = _n;
            _n++;
        }
    }

    public void Remove(uint value)
    {
        if (Contains(value))
        {
            _dense[_sparse[value]] = _dense[_n - 1];
            _sparse[_dense[_n - 1]] = _sparse[value];
            _n--;
        }
    }

    public bool Contains(uint value)
    {
        if (value >= _max || value < 0)
            return false;
        else
            return _sparse[value] < _n && _dense[_sparse[value]] == value;
    }

    public uint Count => _n;

    public uint Current => _dense[_n-1];

    public void Clear() => _n = 0;
}
