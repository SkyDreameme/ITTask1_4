using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaApp.Models;

public class UniversalSet<T>
{
    private readonly HashSet<T> _elements;

    public UniversalSet()
    {
        _elements = new HashSet<T>();
    }

    public int Count => _elements.Count;

    public bool IsEmpty => _elements.Count == 0;

    public void Add(T item)
    {
        _elements.Add(item);
    }

    public void Remove(T item)
    {
        _elements.Remove(item);
    }

    public void Clear()
    {
        _elements.Clear();
    }

    public T[] GetElements()
    {
        return _elements.ToArray();
    }

    public bool Contains(T item)
    {
        return _elements.Contains(item);
    }
}

