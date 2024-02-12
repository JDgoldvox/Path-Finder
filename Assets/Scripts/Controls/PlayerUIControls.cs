using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIControls : MonoBehaviour
{
    Command currentClickCommand = Command.start;

    [SerializeField] private BoardGenerator S_boardGenerator;
    [SerializeField] private BoardActionHub S_boardActionHub;
    PlayerUI S_playerUI;
    InputAction place;
    InputAction step;
    InputAction changeClickCommand;
    int currentClickCommandIndex = 0;
    List<Command> commandList = new List<Command>();

    private void Awake()
    {
        S_playerUI = new PlayerUI();
    }

    private void OnEnable()
    {
        place = S_playerUI.UI.Place;
        place.Enable();
        place.performed += Place;

        step = S_playerUI.UI.Step;
        step.Enable();
        step.performed += Step;

        changeClickCommand = S_playerUI.UI.ChangeClickCommand;
        changeClickCommand.Enable();
        changeClickCommand.performed += ChangeClickCommand;
    }

    private void OnDisable()
    {
        place.Disable();
        step.Disable();
        changeClickCommand.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        commandList.Add(Command.start);
        commandList.Add(Command.end);
        commandList.Add(Command.wall);
        commandList.Add(Command.empty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Place(InputAction.CallbackContext context)
    {
        Vector2 clickScreenLocation = Mouse.current.position.ReadValue();
        Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickScreenLocation.x, clickScreenLocation.y, Camera.main.nearClipPlane));

        CheckForCollisionsWithSquares(clickWorldPosition);
    }

    private void CheckForCollisionsWithSquares(Vector3 clickCoordinates)
    {
        Vector2Int quantizedCoords = ExtraFunctions.QuantizeFloatToInt(clickCoordinates.x, clickCoordinates.y, S_boardGenerator.squareSize, S_boardGenerator.squareSize);

        if(S_boardGenerator.board.TryGetValue(quantizedCoords, out GameObject square))
        {
            S_boardActionHub.SetEndClickCoords(currentClickCommand, clickCoordinates);
            S_boardActionHub.ChangeSquareColour(quantizedCoords, currentClickCommand);
        }
    }

    private void Step(InputAction.CallbackContext context)
    {
        S_boardActionHub.isStep = true;
    }

    private void ChangeClickCommand(InputAction.CallbackContext context)
    {
        currentClickCommandIndex++;
        //reset index if overflow
        if(currentClickCommandIndex > commandList.Count - 1) { currentClickCommandIndex = 0; }
        currentClickCommand = commandList[currentClickCommandIndex];
    }
}