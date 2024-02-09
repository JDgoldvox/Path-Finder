using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        newxy.y = (int)((yInput + +squareSize/2) / resolution);
        return newxy;
    }

}