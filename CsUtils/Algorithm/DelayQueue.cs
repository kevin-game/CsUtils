using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading;

namespace CsUtils.Algorithm;

public class BlockingDelayQueue<T>
{
    private List<DelayQueueItem<T>> _items = new();

    public void Enqueue(T item)
    {
        ArgumentNullException.ThrowIfNull(item, "item != null");
        Enqueue(item, TimeSpan.Zero);
    }

    public void Enqueue(T item, TimeSpan delay)
    {
        ArgumentNullException.ThrowIfNull(item, "item != null");
        ArgumentNullException.ThrowIfNull(delay, "delay != null");
        
        _items.Add(new DelayQueueItem<T>()
        {
            Value = item,
            ReadyTime = DateTime.Now.Add(delay)
        });
    }

    public T? TryDequeue()
    {
        var now = DateTime.Now;
        var item = _items.FirstOrDefault(i => i.ReadyTime <= now);

        if (item == null) return default(T);
        _items.Remove(item);
        return item.Value;
    }
    
    public T? Dequeue()
    {
        return Dequeue(TimeSpan.Zero);
    }

    public T? Dequeue(TimeSpan timeout)
    {
        DateTime startTime = DateTime.Now;

        do
        {
            DateTime now = DateTime.Now;

            var item = _items.FirstOrDefault(i => i.ReadyTime <= now);
            if (item == null)
                continue;

            _items.Remove(item);
            return item.Value;
        } while (DateTime.Now - startTime < timeout);

        return default(T);
    }

    private class DelayQueueItem<TT>
    {
        public TT Value { get; set; }
        public DateTime ReadyTime { get; set; }
    }
} 