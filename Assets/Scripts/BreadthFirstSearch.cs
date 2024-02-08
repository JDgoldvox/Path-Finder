using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BreadthFirstSearch : MonoBehaviour
{
    private BoardGenerator S_boardGenerator;

    private void Awake()
    {
        S_boardGenerator = GetComponent<BoardGenerator>();
    }
    void Start()
    {

    }

    public Vector2Int StartAlgorithm(ref Dictionary<Vector2Int, GameObject> board, Vector2Int start, Vector2Int end)
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

            Console.WriteLine("Visiting {0}", current);

            //get neighbours
            List<Vector2Int> neighbours = GetNeighbours(ref board, current);

            foreach (Vector2Int neighbourSquare in neighbours)
            {
                //if neighbour square not visited nieghbour square, 
                if(!reached.Contains(neighbourSquare)) {
                    frontier.Enqueue(neighbourSquare);
                    reached.Add(neighbourSquare);
                }
            }
        }

        return new Vector2Int();
    }

    private List<Vector2Int> GetNeighbours(ref Dictionary<Vector2Int, GameObject> board, Vector2Int current)
    {

        List<Vector2Int> neighbours = new List<Vector2Int>();

        //remember limits
        Vector2Int search;
        //check top left, top, top right
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                search = new Vector2Int(current.x + x, current.y + y);

                if(search.x < 0) { continue; }
                if(search.y < 0) { continue; }
                if(search.x > S_boardGenerator.boardSize.x - 1) { continue; }
                if(search.y > S_boardGenerator.boardSize.y - 1) { continue; }

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
