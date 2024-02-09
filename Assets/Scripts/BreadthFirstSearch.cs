using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : MonoBehaviour
{
    private BoardGenerator S_boardGenerator;
    private BoardActionHub S_boardActionHub;

    private void Awake()
    {
        S_boardGenerator = GetComponent<BoardGenerator>();
        S_boardActionHub = GetComponent<BoardActionHub>();
    }
    void Start()
    {

    }

    public IEnumerator StartAlgorithm(Dictionary<Vector2Int, GameObject> board, Vector2Int start, Vector2Int end)
    {
        Debug.Log("starting aglor");

        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        frontier.Enqueue(start);

        HashSet<Vector2Int> reached = new HashSet<Vector2Int>();
        reached.Add(start);

        Vector2Int current = new Vector2Int();
            
        //magic happens inside here
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            S_boardActionHub.ChangeSquareColour(current, Command.current);
            //wait seconds
            yield return new WaitForSeconds(0.11f);

            //end condition
            if (current == end)
            {
                break;
            }    

            //get neighbours
            List<Vector2Int> neighbours = GetNeighbours(ref board, current);

            //add to frontier if not already reached
            foreach (Vector2Int neighbourSquare in neighbours)
            {
                //if neighbour square not visited nieghbour square, 
                if (!reached.Contains(neighbourSquare)) {
                    frontier.Enqueue(neighbourSquare);
                    reached.Add(neighbourSquare);

                    S_boardActionHub.ChangeSquareColour(neighbourSquare, Command.frontier);
                    S_boardActionHub.ChangeSquareColour(current, Command.visited);
                }
                //return the current square color to a visited color
                S_boardActionHub.ChangeSquareColour(current, Command.visited);
            }
        }
    }

    private List<Vector2Int> GetNeighbours(ref Dictionary<Vector2Int, GameObject> board, Vector2Int current)
    {

        List<Vector2Int> neighbours = new List<Vector2Int>();

        Vector2Int search;
        //check top left, top, top right
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                search = new Vector2Int(current.x + x, current.y + y);

                //check in bounds
                if(search.x < 0) { continue; }
                if(search.y < 0) { continue; }
                if(search.x > S_boardGenerator.boardSize.x - 1) { continue; }
                if(search.y > S_boardGenerator.boardSize.y - 1) { continue; }

                //check for isWall
                //grab board square script
                if (!board[search].TryGetComponent(out BoardSquare squareScript)){
                    Debug.Log("No Square Script located");
                }
                if (squareScript.isWall) { continue; }
                
                //if this neighbour exists, 
                if (board.TryGetValue(search, out GameObject existingNeighbour))
                {
                    neighbours.Add(search);
                }
            }
        }
        return neighbours;
    }
}