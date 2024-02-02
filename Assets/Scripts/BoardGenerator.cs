using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public Vector2Int boardSize = new Vector2Int();
    public Dictionary<Vector2Int, GameObject> board = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] private GameObject boardSquare;
    private int squareSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateBoard()
    {
        //int totalBoardSize = boardSize.x * boardSize.y;


        //Instantiate a square for each 
        for (int x = 0; x < boardSize.x; x += squareSize)
        {
            for (int y = 0; y < boardSize.y; y += squareSize)
            {
                //instantiate square
                GameObject newSquare = Instantiate(boardSquare, new Vector2(x,y), Quaternion.identity);
                newSquare.transform.localScale = new Vector2(squareSize, squareSize);
                Vector2Int newSquarePosition = ExtraFunctions.QuantizeFloatToInt(new Vector2(newSquare.transform.position.x, newSquare.transform.position.y), squareSize);

                //add square to dictionary
                board[newSquarePosition] = newSquare;

                //get the board square script from game obj
                if (board[newSquarePosition].TryGetComponent<BoardSquare>(out BoardSquare currentSquare))
                {
                    currentSquare.centre = newSquare.transform.position;
                }
                else
                {
                    Debug.Log("This square does not contain a BoardSquare script");
                }
            }
        }

        //test if centre point is working
        foreach (GameObject square in board.Values)
        {
            var obj = Instantiate(boardSquare);
            obj.name = "centre point";
            obj.transform.localScale = new Vector2(0.5f, 0.5f);
            //Debug.Log("Originally: " + obj.transform.position + " after: " + position);
            var script = square.GetComponent<BoardSquare>();
            obj.transform.position = script.centre;
        }
    }
}

