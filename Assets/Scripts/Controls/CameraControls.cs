using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Collections.AllocatorManager;

public class CameraControls : MonoBehaviour
{
    public CameraInputActions S_cameraInputActions;
    private InputAction move;
    private Vector2 moveDirection;
    private float cameraMoveSpeed = 2000f;
    private Rigidbody2D rb;

    private void Awake()
    {
        S_cameraInputActions = new CameraInputActions();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        move = S_cameraInputActions.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        moveDirection = move.ReadValue<Vector2>();

        rb.velocity = new Vector2(moveDirection.x * cameraMoveSpeed * Time.deltaTime,
            moveDirection.y * cameraMoveSpeed * Time.deltaTime);
    }
}
