using UnityEngine;
using TMPro;

public class BoardSquare : MonoBehaviour
{
    [HideInInspector] public Vector2 centre = new Vector2();
    [HideInInspector] public bool isWall = false;
    [SerializeField] private TMP_Text costText;
    public float cost = 0;

    public BoardSquare(float x, float y)
    {
        this.centre.x = x;
        this.centre.y = y;
    }

    public void UpdateCost(float cost)
    {
        this.cost = cost;
        costText.text = (cost.ToString());
    }
}
