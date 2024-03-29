using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStar : MonoBehaviour
{
    private BoardGenerator S_boardGenerator;
    private BoardActionHub S_boardActionHub;
    private bool algorithmFinished = false;
    Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
    Vector2Int targetSquare;
    private float speedOfStep = 0.11f;

    public void ChangeSpeedOfStep(float speed)
    {
        speedOfStep = speed;
    }
    private void Awake()
    {
        S_boardGenerator = GetComponent<BoardGenerator>();
        S_boardActionHub = GetComponent<BoardActionHub>();
    }

    public IEnumerator StartAlgorithm(Dictionary<Vector2Int, GameObject> board, Vector2Int start, Vector2Int end)
    {
        targetSquare = end;

        PriorityQueue<float, Vector2Int> frontier = new PriorityQueue<float, Vector2Int>();
        frontier.Enqueue(0, start);

        Dictionary<Vector2Int, float> cost = new Dictionary<Vector2Int, float>();
        cost[start] = 0;

        cameFrom.Clear();
        cameFrom[start] = new Vector2Int(-1, -1);
            
        Vector2Int current = new Vector2Int();

        //magic happens inside here
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            S_boardActionHub.ChangeSquareColour(current, Command.current);
            //wait seconds
            yield return new WaitForSeconds(speedOfStep);

            //end condition
            if (current == end)
            {
                break;
            }

            //get neighbours
            List<Vector2Int> neighbours = GetNeighbours(ref board, current);

            //add to frontier if not already reached
            foreach (Vector2Int next in neighbours)
            {
                S_boardActionHub.ChangeSquareColour(current, Command.visited);

                float newCost = cost[current] + 1f;

                //if neighbour square not visited nieghbour square, 
                if (!cost.ContainsKey(next) || (newCost < cost[next]))
                {
                    cost[next] = newCost;
                    S_boardActionHub.SetSquareCost(next, newCost);

                    float heuristic = ManhattanDistance(next, end) + EuclideanDistance(next, end) + 2 * OctileHeuristic(next,end); 
                    heuristic /= 4;
                    float priority = heuristic + newCost;

                    frontier.Enqueue(priority, next);
                    cameFrom[next] = current;

                    S_boardActionHub.ChangeSquareColour(next, Command.frontier);
                }
            }
        }

        algorithmFinished = true;

        List<Vector2Int> correctPath = new List<Vector2Int>();

        Vector2Int previous = new Vector2Int(-2, -2);
        Vector2Int lastCoord = new Vector2Int(-1, -1);
        previous = targetSquare;

        while (previous != lastCoord)
        {
            previous = cameFrom[previous];
            correctPath.Add(previous);
        }
        correctPath.Remove(lastCoord);

        correctPath.Reverse();

        foreach (Vector2Int square in correctPath)
        {
            S_boardActionHub.ChangeSquareColour(square, Command.bestPath);
        }

        S_boardActionHub.PassBestPath(correctPath);
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
                if (search.x < 0) { continue; }
                if (search.y < 0) { continue; }
                if (search.x > S_boardGenerator.boardSize.x - 1) { continue; }
                if (search.y > S_boardGenerator.boardSize.y - 1) { continue; }

                //check for isWall
                //grab board square script
                if (!board[search].TryGetComponent(out BoardSquare squareScript))
                {
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

    private float ManhattanDistance(Vector2Int start, Vector2Int end)
    {
        return Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
    }

    private float EuclideanDistance(Vector2Int start, Vector2Int end)
    {
        float x = Mathf.Pow(end.x - start.x, 2);
        float y = Mathf.Pow(end.y - start.y, 2);
        return x + y;
    }

    //https://github.com/riscy/a_star_on_grids/blob/master/src/heuristics.cpp#L59
    private float OctileHeuristic(Vector2Int start, Vector2Int end)
    {
        float dx = Mathf.Abs(start.x - end.x);
        float dy = Mathf.Abs(start.y - end.y);
        float twoCardinalMinusDiagonal = 2 * 1 - Mathf.Sqrt(2);
        return (twoCardinalMinusDiagonal * Mathf.Abs(dx - dy) + 1 + Mathf.Sqrt(2) * (dx + dy)) / 2;
    }


}