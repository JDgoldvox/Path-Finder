using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BoardGenerator : MonoBehaviour
{
    public Vector2Int boardSize = new Vector2Int();
    public Dictionary<Vector2Int, GameObject> board = new Dictionary<Vector2Int, GameObject>();

    [SerializeField] private GameObject boardSquare;
    [SerializeField] private GameObject dot;

    [HideInInspector] public int squareSize = 1;
    [HideInInspector] public float squareScale = 2.5f;
    public Dictionary<Vector2Int, GameObject> GenerateBoard()
    {
        //Instantiate a square for each 
        for (int x = 0; x < boardSize.x; x += squareSize)
        {
            for (int y = 0; y < boardSize.y; y += squareSize)
            {
                //instantiate square
                GameObject newSquare = Instantiate(boardSquare, new Vector2(x,y), Quaternion.identity);
                newSquare.transform.localScale = new Vector2(newSquare.transform.localScale.x, newSquare.transform.localScale.y);
                Vector2Int newSquarePosition = ExtraFunctions.QuantizeFloatToInt(new Vector2(x, y), squareSize, squareSize);
                newSquare.transform.parent = transform;

                //add square to dictionary
                board[newSquarePosition] = newSquare;

                //get the board square script from game obj
                if (board[newSquarePosition].TryGetComponent(out BoardSquare currentSquare))
                {
                    currentSquare.centre = newSquare.transform.position;
                }
                else
                {
                    Debug.Log("This square does not contain a BoardSquare script");
                }
            }
        }

        //test if centre point is working, there should be a small dot in the centre
        float scaleOfCentreDot = (float)squareSize / 4;
        foreach (GameObject square in board.Values)
        {
            //create the dot object and name it centre point
            var obj = Instantiate(dot);
            obj.name = "centre point";
            obj.transform.parent = square.transform;

            //scale the dot
            obj.transform.localScale = new Vector2(scaleOfCentreDot * squareSize, scaleOfCentreDot * squareSize);

            //grab the script that holds the square
            if (!square.TryGetComponent(out BoardSquare squareScript))
            {
                Debug.Log("Square script does not exist");
                return null;
            }

            //change color of dot
            if (!dot.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                Debug.Log("Dot does not have sprite renderer");
                return null;
            }
            dot.GetComponent<SpriteRenderer>().color = Color.black;

            //transform dot to centre of square using float coords
            obj.transform.position = squareScript.centre;
        }

        return board;
    }
}