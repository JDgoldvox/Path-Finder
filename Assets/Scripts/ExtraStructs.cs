using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtraFunctions
{
    public static Vector2Int QuantizeFloatToInt(Vector2 xy, int resolution)
    {
        Vector2Int newxy = new Vector2Int();
        newxy.x = (int)(xy.x / resolution);
        newxy.y = (int)(xy.y / resolution);
        return newxy;
    }
}