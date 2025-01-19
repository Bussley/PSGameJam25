using System.Collections.Generic;
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
    private InputActionReference playerInput;

    private Vector2 moveDirection;

    //Temp, get rid after testing or move it to better location
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private GameObject wheatTile;

    private void Start() {

    }

    private void Update() {
        moveDirection = playerInput.action.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        rig.linearVelocity = new Vector2(moveDirection.x * moveSpeed,
                                         moveDirection.y * moveSpeed);
    }

    public void TestPlaceWhate(InputAction.CallbackContext context) {
        //Test logic
        if (context.performed)
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int grid_pos = map.WorldToCell(mouse_pos);
            //Debug.Log("Position at" + grid_pos);

            GameObject gameObjectAtPosition = map.GetInstantiatedObject(grid_pos);
            if (gameObjectAtPosition == null)
            {
                map.SetTile(grid_pos, new Tile() { gameObject = wheatTile });
                Debug.Log("test");
            }
            //map.SetTile(grid_pos, new Tile() { gameObject = wheatTile });


        }
    }
}
