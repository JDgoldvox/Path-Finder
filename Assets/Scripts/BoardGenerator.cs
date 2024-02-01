using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public Vector2Int boardSize = new Vector2Int();
    public Dictionary<Vector2Int, GameObject> board = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] private GameObject boardSquare;

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
        int totalBoardSize = boardSize.x * boardSize.y;

        //Instantiate a square for each 
        for (int x = 0; x < totalBoardSize; x++)
        {
            for (int y = 0; y < totalBoardSize; y++)
            {
                //instantiate square
                GameObject newSquare = Instantiate(boardSquare, new Vector3(x,y,0), Quaternion.identity);

                //add square to dictionary
                board[new Vector2Int(x,y)] = newSquare;
            }
        }

        //boardCoords = new GameObject[boardSize.x,boardSize.y];
    }
}

