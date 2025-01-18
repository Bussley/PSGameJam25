using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rig;

    [SerializeField]
    private float move_speed;

    [SerializeField]
    private InputActionReference player_input;

    private Vector2 move_direction;

    private void Start() {
        
    }

    private void Update() {
        move_direction = player_input.action.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        rig.linearVelocity = new Vector2(move_direction.x * move_speed,
                                         move_direction.y * move_speed);
    }
}
