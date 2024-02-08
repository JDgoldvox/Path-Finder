using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIControls : MonoBehaviour
{
    enum ClickCommand
    {
        start,end
    }

    [SerializeField] private BoardGenerator S_boardGenerator;
    [SerializeField] private BoardActionHub S_boardActionHub;
    PlayerUI S_playerUI;
    InputAction placeStartLocation;
    InputAction placeEndLocation;
    InputAction step;

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

        step = S_playerUI.UI.Step;
        step.Enable();
        step.performed += Step;
    }

    private void OnDisable()
    {
        placeStartLocation.Disable();
        placeEndLocation.Disable();
        step.Disable();

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

        CheckForCollisionsWithSquares(clickWorldPosition, ClickCommand.start);
    }

    private void PlaceEndLocation(InputAction.CallbackContext context)
    {
        Vector2 clickScreenLocation = Mouse.current.position.ReadValue();
        Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickScreenLocation.x, clickScreenLocation.y, Camera.main.nearClipPlane));

        CheckForCollisionsWithSquares(clickWorldPosition, ClickCommand.end);
    }

    private void CheckForCollisionsWithSquares(Vector3 clickCoordinates, ClickCommand command)
    {
        if (S_boardActionHub.hasStartSquare && S_boardActionHub.hasEndSquare) { return; }

        Vector2Int quantizedCoords = ExtraFunctions.QuantizeFloatToInt(clickCoordinates.x, clickCoordinates.y, S_boardGenerator.squareSize, S_boardGenerator.squareSize);

        if(S_boardGenerator.board.TryGetValue(quantizedCoords, out GameObject square))
        {
            if(command == ClickCommand.start)
            {
                S_boardActionHub.startSquare = quantizedCoords;
                S_boardActionHub.hasStartSquare = true;
                S_boardActionHub.ChangeSquareColour(quantizedCoords, Color.red);
            }
            else if(command == ClickCommand.end)
            {
                S_boardActionHub.endSquare = quantizedCoords;
                S_boardActionHub.hasEndSquare = true;
                S_boardActionHub.ChangeSquareColour(quantizedCoords, Color.green);
            }
        }
    }

    private void Step(InputAction.CallbackContext context)
    {
        S_boardActionHub.isStep = true;
    }
}