using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BreadthFirstSearch : MonoBehaviour
{
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

        //check top left, top, top right
        Vector2Int search = new Vector2Int(current.x - 1, current.y);
        
        //if this neighbour exists, 
        if(board.TryGetValue(search, out GameObject topLeftNeighbour)){
            neighbours.Add(search);
        }

        //check middle left, middle right,
        //check bottom left, bottom, bottom right.

        return new List<Vector2Int>();
    }

}
