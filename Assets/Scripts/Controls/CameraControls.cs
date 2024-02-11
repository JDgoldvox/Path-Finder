using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public CameraInputActions S_cameraInputActions;
    private InputAction move;
    private Vector2 moveDirection;
    private float cameraMoveSpeed = 2000f;
    private Rigidbody2D rb;
    private InputAction zoom;
    private float zoomValue = -50;
    private float zoomSpeed = 1f;

    private void Awake()
    {
        S_cameraInputActions = new CameraInputActions();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        move = S_cameraInputActions.Player.Move;
        move.Enable();

        zoom = S_cameraInputActions.Player.Zoom;
        zoom.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        zoom.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        Zoom();
    }

    private void CameraMovement()
    {
        moveDirection = move.ReadValue<Vector2>();

        rb.velocity = new Vector2(moveDirection.x * cameraMoveSpeed * Time.deltaTime,
            moveDirection.y * cameraMoveSpeed * Time.deltaTime);
    }

    private void Zoom()
    {
        //if no value has changed
        if (zoomValue == zoom.ReadValue<float>())
        {
            return;
        }

        zoomValue = zoom.ReadValue<float>();

        Vector3 cameraPosition = Camera.main.transform.position;
        if (zoomValue < -1)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + zoomSpeed;
        }
        else if(zoomValue > 1) 
        {
            //don't zoom if 1 
            if(Camera.main.orthographicSize == 1)
            {
                return;
            }

            Camera.main.orthographicSize = Camera.main.orthographicSize - zoomSpeed;
        }
    }
}
