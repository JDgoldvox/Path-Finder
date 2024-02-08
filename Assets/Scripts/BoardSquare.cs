using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquare : MonoBehaviour
{
    public Vector2 centre = new Vector2();
    public bool isWall = false;

    public BoardSquare(float x, float y)
    {
        this.centre.x = x;
        this.centre.y = y;
    }
}
