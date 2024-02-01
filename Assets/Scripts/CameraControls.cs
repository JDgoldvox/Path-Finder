using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public CameraInputActions S_cameraInputActions;
    private InputAction move;
    private InputAction fire;
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

        fire = S_cameraInputActions.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
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

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("We fired!");
    }

    private void CameraMovement()
    {
        moveDirection = move.ReadValue<Vector2>();

        rb.velocity = new Vector2(moveDirection.x * cameraMoveSpeed * Time.deltaTime,
            moveDirection.y * cameraMoveSpeed * Time.deltaTime);
    }
}
