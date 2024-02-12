using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private BoardActionHub S_BoardActionHub;
    [SerializeField] private GameObject marker;
    private float speed = 0.015f;
    private float distanceTravelled = 0f;
    private float distancePerDot = 0.1f;

    public void ActivateAgent(Dictionary<Vector2Int, GameObject> board, List<Vector2Int> squaresToVisit, Vector2Int start, Vector2Int end)
    {
        foreach(Vector3Int vector3Int in squaresToVisit)
        {
            Debug.Log(vector3Int);
        }
        //position agent at start
        BoardSquare startScript = board[start].GetComponent<BoardSquare>();
        transform.position = startScript.centre;

        StartCoroutine(MoveToTargets(board, squaresToVisit, end));
    }

    private IEnumerator MoveToTargets(Dictionary<Vector2Int, GameObject> board, List<Vector2Int> squaresToVisit, Vector2Int end)
    {
        foreach (Vector2Int v2 in squaresToVisit)
        {
            BoardSquare script = board[v2].GetComponent<BoardSquare>();
            Vector2 positionToMove = script.centre;

            yield return StartCoroutine(MoveAgent(positionToMove));
        }

        BoardSquare lastScript = board[end].GetComponent<BoardSquare>();
        Vector2 lastPositionToMove = lastScript.centre;

        yield return StartCoroutine(MoveAgent(lastPositionToMove));
    }

    private IEnumerator MoveAgent(Vector2 positionToMove)
    {
        Vector2 lastPosition = transform.position;

        while ((Vector2)transform.position != positionToMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, positionToMove, speed);
            LeaveDot(lastPosition, transform.position);
            lastPosition = transform.position;
            yield return null; //Wait for the end of the frame before continuing the loop
        }
    }

    private void LeaveDot(Vector2 before, Vector2 now)
    {
        float distanceSinceLastUpdate = Vector2.Distance(before, now);
        distanceTravelled += distanceSinceLastUpdate;

        if (distanceTravelled >= distancePerDot)
        {
            Instantiate(marker, transform.position, Quaternion.identity);
        }
    }
}
