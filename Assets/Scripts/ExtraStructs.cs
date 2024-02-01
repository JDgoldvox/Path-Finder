using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardSquare{
    public Vector2 centre;

    public BoardSquare(float x, float y)
    {
        this.centre.x = x;
        this.centre.y = y;
    }
}

public static class ExtraFunctions
{
    public static Vector2 QuantizeFloatToInt(Vector2 xy, float resolution)
    {
        Vector2Int newxy = new Vector2Int();
        newxy.x = (int)(xy.x / resolution);
        newxy.y = (int)(xy.y / resolution);
        return newxy;
    }
}