using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private enum Direction
    {
        N, NE, E, SE, S, SW, W, NW,
        None
    };

    [SerializeField]
    private Rigidbody2D rig;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private InputActionReference playerInput;

    private Vector2 moveDirection;
    private Direction playerDirection;
    private bool canMove;

    //Temp, get rid after testing or move it to better location
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private GameObject wheatTile;


    private void Start() {
        playerDirection = Direction.S;
        canMove = true;
    }

    private void Update() {

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
            int dir = (int)(Vector2.Angle(Vector2.up, moveDirection) / 45.0f);

            if (dir == 0)
                playerDirection = Direction.N;
            else if (dir == 4)
                playerDirection = Direction.S;
            else if (moveDirection.x < 0)
            {
                if (dir == 1)
                    playerDirection = Direction.NW;
                else if (dir == 2)
                    playerDirection = Direction.W;
                else if (dir == 3)
                    playerDirection = Direction.SW;
            }
            else if (moveDirection.x > 0)
            {
                if (dir == 1)
                    playerDirection = Direction.NE;
                else if (dir == 2)
                    playerDirection = Direction.E;
                else if (dir == 3)
                    playerDirection = Direction.SE;
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

        }
        else if (context.canceled) {
            canMove = true;
        }
    }

    public void TestPlaceWhate(InputAction.CallbackContext context) {
        //Test logic
        if (context.performed)
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int grid_pos = map.WorldToCell(mouse_pos);

            GameObject gameObjectAtPosition = map.GetInstantiatedObject(grid_pos);
            if (gameObjectAtPosition == null)
            {
                map.SetTile(grid_pos, new Tile() { gameObject = wheatTile });
                Debug.Log("test");
            }
        }
    }
}
