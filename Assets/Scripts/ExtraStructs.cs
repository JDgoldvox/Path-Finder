using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum Command
{
    start, end, wall, empty, frontier, visited, bestPath, current
}

public static class ExtraFunctions
{
    public static Vector2Int QuantizeFloatToInt(Vector2 xy, int resolution, float squareSize)
    {
        Vector2Int newxy = new Vector2Int();
        newxy.x = (int)((xy.x + squareSize/2) / resolution);
        newxy.y = (int)((xy.y + squareSize/2) / resolution);
        return newxy;
    }

    public static Vector2Int QuantizeFloatToInt(float xInput, float yInput, int resolution, float squareSize)
    {
        Vector2Int newxy = new Vector2Int();
        newxy.x = (int)((xInput + squareSize/2) / resolution);
        newxy.y = (int)((yInput + squareSize/2) / resolution);
        return newxy;
    }

}


/// <summary>
/// Priority queue created by Chat GPT
/// </summary>
/// <typeparam name="TPriority"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class PriorityQueue<TPriority, TValue>
{
    private SortedDictionary<TPriority, Queue<TValue>> dictionary = new SortedDictionary<TPriority, Queue<TValue>>();

    public void Enqueue(TPriority priority, TValue value)
    {
        if (!dictionary.ContainsKey(priority))
        {
            dictionary[priority] = new Queue<TValue>();
        }
        dictionary[priority].Enqueue(value);
    }

    public TValue Dequeue()
    {
        if (dictionary.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }
        var first = dictionary.First();
        var value = first.Value.Dequeue();
        if (first.Value.Count == 0)
        {
            dictionary.Remove(first.Key);
        }
        return value;
    }

    public int Count
    {
        get { return dictionary.Sum(q => q.Value.Count); }
    }

    public bool IsEmpty => dictionary.Count == 0;
}

public struct Vector2IntIntPair
{
    public Vector2Int vector2Int;
    public float intValue;

    public Vector2IntIntPair(Vector2Int vector2Int, float intValue)
    {
        this.vector2Int = vector2Int;
        this.intValue = intValue;
    }
}