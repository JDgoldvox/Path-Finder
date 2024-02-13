using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlgorithmType
{
    bfs, dijkstra, astar
}
public class DropDown : MonoBehaviour
{
    [SerializeField] private BoardActionHub S_BoardActionHub;
    public void ChangeAlgorithm(int val)
    {
        if(val == 0)
        {
            S_BoardActionHub.ChangeAlgorithmType(AlgorithmType.bfs);
        }
        else if (val == 1)
        {
            S_BoardActionHub.ChangeAlgorithmType(AlgorithmType.dijkstra);
        }
        else if(val == 2)
        {
            S_BoardActionHub.ChangeAlgorithmType(AlgorithmType.astar);
        }
        else {
            S_BoardActionHub.ChangeAlgorithmType(AlgorithmType.bfs);
        }
    }
}
