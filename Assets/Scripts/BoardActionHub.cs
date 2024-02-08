using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardActionHub : MonoBehaviour
{
    private BreadthFirstSearch S_bfs;
    
    private BoardGenerator S_boardGenerator;
    private Dictionary<Vector2Int, GameObject> board;
    private bool hasStartSquare = false, hasEndSquare = false;
    [HideInInspector] public bool hasCompleteInits = false;
    [HideInInspector] public Vector2Int startSquare = new Vector2Int(0, 0);
    [HideInInspector] public Vector2Int endSquare = new Vector2Int(0, 0);

    [SerializeField] private Vector4 frontierSquareColor, visitedColor, bestPathColor;
    [SerializeField] private Vector4 startSquareColor, endSquareColor, wallSquareColor, emptySquareColor, wrongSquareColor;
    [HideInInspector] public bool isStep = false;
    private bool isContinuousStep = false;

    private void Awake()
    {
        S_boardGenerator = GetComponent<BoardGenerator>();
        S_bfs = GetComponent<BreadthFirstSearch>();
    }

    // Start is called before the first frame update
    void Start()
    {
        board = S_boardGenerator.GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        //isEverythingComplete();
        
        //if(hasStartSquare && hasEndSquare)
        //{
        //    if (!isStep)
        //    {
        //        return;
        //    }
        //    isStep = false;

        //    Debug.Log("starting");
        //    Algorithm();
        //}
    }

    void Algorithm()
    {
        S_bfs.StartAlgorithm(ref board, startSquare, endSquare);
    }

    public void ChangeSquareColour(Vector2Int squarePosition, ClickCommand clickCommand)
    {
        Color colorChange = new Color32();

        if (clickCommand == ClickCommand.start) {
            startSquare = squarePosition;
            hasStartSquare = true;
            colorChange = startSquareColor; 
        }
        else if (clickCommand == ClickCommand.end) 
        {
            endSquare = squarePosition;
            hasEndSquare = true;
            colorChange = endSquareColor;
        }
        else if (clickCommand == ClickCommand.wall)
        {
            colorChange = wallSquareColor; 
        }
        else if (clickCommand == ClickCommand.empty)
        {
            colorChange = emptySquareColor; 
        }
        else { 
            colorChange = wrongSquareColor;
            Debug.Log("This color change does not have a click command. Action undefined.");
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
        if(clickCommand == ClickCommand.wall)
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

}