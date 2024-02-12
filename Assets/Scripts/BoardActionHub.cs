using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardActionHub : MonoBehaviour
{
    private BreadthFirstSearch S_bfs;
    private Dijkstra S_Dijkstra;
    private AStar S_AStar;

    [SerializeField] private Agent S_Agent;
    private BoardGenerator S_boardGenerator;
    private Dictionary<Vector2Int, GameObject> board;
    private bool hasStartSquare = false, hasEndSquare = false;
    [HideInInspector] public bool hasCompleteInits = false;
    [HideInInspector] public Vector2Int startSquare = new Vector2Int(0, 0);
    [HideInInspector] public Vector2Int endSquare = new Vector2Int(0, 0);

    [SerializeField] private Vector4 frontierSquareColor, visitedColor, bestPathColor, currentSquareColor;
    [SerializeField] private Vector4 startSquareColor, endSquareColor, wallSquareColor, emptySquareColor, wrongSquareColor;
    [HideInInspector] public bool isStep = false;
    private bool isContinuousStep = false;
    private void Awake()
    {
        S_boardGenerator = GetComponent<BoardGenerator>();
        S_bfs = GetComponent<BreadthFirstSearch>();
        S_Dijkstra = GetComponent<Dijkstra>();
        S_AStar = GetComponent<AStar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        board = S_boardGenerator.GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEverythingComplete()) { return; }

        if (!isStep)
        {
            return;
        }
        isStep = false;

        Debug.Log("starting");
        Algorithm();
    }

    void Algorithm()
    {
        //StartCoroutine(S_bfs.StartAlgorithm(board, startSquare, endSquare));
        StartCoroutine(S_Dijkstra.StartAlgorithm(board, startSquare, endSquare));
        //StartCoroutine(S_AStar.StartAlgorithm(board, startSquare, endSquare));
    }

    public void ChangeSquareColour(Vector2Int squarePosition, Command command)
    {
        Color colorChange = new Color();

        if (command == Command.start) 
        {
            ResetSquare(startSquare);
            startSquare = squarePosition;
            hasStartSquare = true;
            colorChange = startSquareColor;

            //check if we replaced end
            if (endSquare == startSquare) { hasEndSquare = false; }
        }
        else if (command == Command.end) 
        {
            ResetSquare(endSquare);
            endSquare = squarePosition;
            hasEndSquare = true;
            colorChange = endSquareColor;

            //check if we replaced start
            if(endSquare == startSquare){ hasStartSquare = false; }
        }
        else if (command == Command.wall)
        {
            colorChange = wallSquareColor; 
        }
        else if (command == Command.empty)
        {
            ResetSquare(squarePosition);
        }
        else if(command == Command.frontier)
        {
            //Don't over write the color of the start or end square
            if (squarePosition == startSquare || squarePosition == endSquare) { return; }

            colorChange = frontierSquareColor;
        }
        else if (command == Command.visited)
        {
            //Don't over write the color of the start or end square
            if (squarePosition == startSquare || squarePosition == endSquare) { return; }

            colorChange = visitedColor;
        }
        else if (command == Command.bestPath)
        {
            //Don't over write the color of the start or end square
            if (squarePosition == startSquare || squarePosition == endSquare) { return; }

            colorChange = bestPathColor;
        }
        else if(command == Command.current)
        {
            //Don't over write the color of the start or end square
            if (squarePosition == startSquare || squarePosition == endSquare) { return; }

            colorChange = currentSquareColor;
        }
        else { 
            colorChange = wrongSquareColor;
            Debug.Log("This color change does not have a command. Action undefined.");
        }

        //get the game object's script
        if (!board.TryGetValue(squarePosition, out GameObject squareToChange))
        {
            Debug.Log("Unable to get value to change colour");
            return;
        }

        //change color
        SpriteRenderer sr = squareToChange.transform.GetChild(0).GetComponent<SpriteRenderer>();
        if(sr == null)
        {
            Debug.Log("Sprite renderer is null! Can't change color");
            return;
        }
        sr.color = colorChange;

        //change wall status
        if(command == Command.wall)
        {
            if(!squareToChange.TryGetComponent(out BoardSquare boardSquareScript))
            {
                Debug.Log("Cannot grab square script");
            }
            boardSquareScript.isWall = true;
        }
    }
    public void ContinuousStep()
    {
        isContinuousStep = !isContinuousStep;
    }

    private bool isEverythingComplete()
    {
        return hasStartSquare && hasEndSquare ? true : false;
    }

    private void ResetSquare(Vector2Int squarePosition) //reset previous start square to orginal state
    {
        GameObject square = board[squarePosition];

        //grab script
        if (!square.TryGetComponent(out BoardSquare squareScript))
        {
            Debug.Log("No BoardSquare script located");
            return;
        }

        //change wall status
        squareScript.isWall = false;

        //grab child 
        SpriteRenderer sr = square.transform.GetChild(0).GetComponent<SpriteRenderer>();

        //reset to default color
        sr.color = emptySquareColor;
    }

    public void PassBestPath(List<Vector2Int> pathPassed)
    {
        S_Agent.ActivateAgent(board, pathPassed, startSquare, endSquare);
    }
}