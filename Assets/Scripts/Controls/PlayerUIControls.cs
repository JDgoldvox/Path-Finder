using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUIControls : MonoBehaviour
{
    [SerializeField] private BoardGenerator S_boardGenerator;
    PlayerUI S_playerUI;
    InputAction placeStartLocation;
    InputAction placeEndLocation;

    private void Awake()
    {
        S_playerUI = new PlayerUI();
    }

    private void OnEnable()
    {
        placeStartLocation = S_playerUI.UI.PlaceStartLocation;
        placeStartLocation.Enable();
        placeStartLocation.performed += PlaceStartLocation;

        placeEndLocation = S_playerUI.UI.PlaceEndLocation;
        placeEndLocation.Enable();
        placeEndLocation.performed += PlaceEndLocation;
    }

    private void OnDisable()
    {
        placeStartLocation.Disable();
        placeEndLocation.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaceStartLocation(InputAction.CallbackContext context)
    {
        Vector2 clickScreenLocation = Mouse.current.position.ReadValue();
        Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickScreenLocation.x, clickScreenLocation.y, 0f));

        Debug.Log("Clicked on : " + clickWorldPosition);

        CheckForCollisionsWithSquares(clickWorldPosition);
    }

    private void PlaceEndLocation(InputAction.CallbackContext context)
    {
        Vector2 clickScreenLocation = Mouse.current.position.ReadValue();
        Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickScreenLocation.x, clickScreenLocation.y, Camera.main.nearClipPlane));

        //Debug.Log("end : " + clickWorldPosition);

        CheckForCollisionsWithSquares(clickWorldPosition);
    }

    private void CheckForCollisionsWithSquares(Vector3 clickCoordinates)
    {
        Vector2Int quantizedCoords = ExtraFunctions.QuantizeFloatToInt(clickCoordinates.x, clickCoordinates.y, S_boardGenerator.squareSize, S_boardGenerator.squareSize);

        if(S_boardGenerator.board.TryGetValue(quantizedCoords, out GameObject square))
        {
            Debug.Log("SQUARE FOUND: COORDS:" + quantizedCoords);
            Debug.Log("actual centre:" + square.GetComponent<BoardSquare>().centre);

            //change color of dot
            if (!square.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                Debug.Log("Dot does not have sprite renderer");
                return;
            }
            square.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
