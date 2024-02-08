using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardActionHub : MonoBehaviour
{
    private BreadthFirstSearch S_bfs;
    
    private BoardGenerator S_boardGenerator;
    private Dictionary<Vector2Int, GameObject> board;
    [HideInInspector] public bool hasStartSquare = false, hasEndSquare = false;
    [HideInInspector] public Vector2Int startSquare = new Vector2Int(0, 0);
    [HideInInspector] public Vector2Int endSquare = new Vector2Int(0, 0);

    [SerializeField] private Color frontierTileColor, visitedColor, bestPathColor;
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
        if(hasStartSquare && hasEndSquare)
        {
            if (!isStep)
            {
                return;
            }
            isStep = false;

            Debug.Log("starting");
            Algorithm();
        }
    }

    void Algorithm()
    {
        S_bfs.StartAlgorithm(ref board, startSquare, endSquare);
    }

    public void ChangeSquareColour(Vector2Int squarePosition, Color colour)
    {
        //get the game object's script
        if(!board.TryGetValue(squarePosition, out GameObject squareToChangeColour))
        {
            Debug.Log("Unable to get value to change colour");
            return;
        }

        squareToChangeColour.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colour;
    }

    public void ContinuousStep()
    {
        isContinuousStep = !isContinuousStep;
    }

}
