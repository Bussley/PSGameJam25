using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rig;

    [SerializeField]
    private float moveSpeed;

        [SerializeField]
    public string CurrentWeapon {
        get { return CurrentWeapon; }
        set { CurrentWeapon = CurrentWeapon; }
    }

    [SerializeField]
    private InputActionReference playerInput;

    [SerializeField]
    private GameObject laserPrefab;

    private Vector2 moveDirection;

    // N = 0, NE = 1, E = 2, SE = 3
    // S = 4, NW = 5, W = 6, SW = 7
    private int playerDirection;
    private bool canMove;

    private GameObject laserGO;

    private void Start() {
        playerDirection = 4;
        canMove = true;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Test input key");
            Debug.Log(Input.GetKey(KeyCode.Space));
        }

    }

    private void FixedUpdate() {
        rig.linearVelocity = new Vector2(moveDirection.x * moveSpeed,
                                         moveDirection.y * moveSpeed);
    }

    public void DetermineDirection(InputAction.CallbackContext context) {
        // Get direction of player and move
        if (context.performed && canMove)
        {
            moveDirection = playerInput.action.ReadValue<Vector2>();
            playerDirection = (int)(Vector2.Angle(Vector2.up, moveDirection) / 45.0f);

            if (moveDirection.x < 0)
            {
                playerDirection += 4;
            }
            Debug.Log(playerDirection);
        }
        else if(context.canceled)
            moveDirection = Vector2.zero;
    }

    public void FireLaser(InputAction.CallbackContext context) {
        if(context.started) {
            // Stop Player
            moveDirection = Vector2.zero;
            canMove = false;

            // Spawn aimer
            laserGO = Instantiate(laserPrefab, transform);
            laserGO.GetComponent<LaserLogic>().Charge(playerDirection);
        }
        else if (context.canceled) {
            laserGO.GetComponent<LaserLogic>().Fire();
            canMove = true;
        }

    }

    public void TestPlaceWhate(InputAction.CallbackContext context) {
        //Test logic
        if (context.performed)
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            TileManager.ChangeTile(mouse_pos);
        }
    }

    public void SwitchWeapons (InputAction.CallbackContext context) 
    {
        // context.control will have 3 actions/ output. action, cancel, something else.
        Debug.Log(context.control); 
    }
    
}
