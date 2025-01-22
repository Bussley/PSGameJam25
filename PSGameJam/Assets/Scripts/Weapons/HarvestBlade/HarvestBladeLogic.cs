using UnityEngine;

public class HarvestBladeLogic : MonoBehaviour
{
    [SerializeField] 
    private float arcLength;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Vector2 rotatePoint;

    private float duration;
    private float curTime;
    private Vector3 offsetPosition;

    private void Awake() {
        curTime = 0;
        offsetPosition = transform.parent.position + new Vector3(rotatePoint.x, rotatePoint.y, 0);
    }

    private void Update() {

        offsetPosition = transform.parent.position + new Vector3(rotatePoint.x, rotatePoint.y, 0);

        if (curTime < duration)
            transform.RotateAround(offsetPosition, Vector3.forward, -rotateSpeed);
        else
        {
            Destroy(gameObject);
        }

        curTime++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // HIT A CROP TO HARVEST. LOGIC TO CALL CROP.HARVEST HERE
        Debug.Log("HIT");
    }

    public void Fire(Vector2 direction) {
        float angle = Vector2.Angle(Vector2.left, direction);

        // Flip if face positive direction
        if (direction.y > 0)
            angle = -angle;

        // This is to get the correct X for hand swipe with sword
        Vector3 xRot = TileManager.rotate(new Vector2(-rotatePoint.x, 0), angle);

        angle += arcLength;
        duration = (arcLength * 2) / rotateSpeed;

        // Set position to offset Position
        offsetPosition += xRot;
        transform.position = offsetPosition - new Vector3(transform.lossyScale.x/2, 0);

        transform.RotateAround(offsetPosition, Vector3.forward, angle);
    }
}
