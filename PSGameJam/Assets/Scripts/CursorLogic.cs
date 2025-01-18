using UnityEngine;
using UnityEngine.InputSystem;

public class CursorLogic : MonoBehaviour
{
    [SerializeField]
    private float mouse_x_offset = 0.0f;
    [SerializeField]
    private float mouse_y_offset = 0.0f;

    [SerializeField]
    private InputActionReference cursor_input;

    private void Start() {
        Cursor.visible = false;
    }

    private void Update() {
        // Get screen mouse position and set object to that world position
        var cursor_pos = Camera.main.ScreenToWorldPoint(cursor_input.action.ReadValue<Vector2>());
        transform.position = new Vector2(cursor_pos.x + mouse_x_offset, cursor_pos.y + mouse_y_offset);
    }
}
