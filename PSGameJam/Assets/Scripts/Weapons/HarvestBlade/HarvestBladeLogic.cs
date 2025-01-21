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

        if (curTime < duration)
            transform.RotateAround(offsetPosition, Vector3.forward, -rotateSpeed);
        else
        {
            transform.parent.GetComponent<PlayerController>().SetMove(true);
            Destroy(gameObject);
        }

        curTime++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // HIT A CROP TO HARVEST. LOGIC TO CALL CROP.HARVEST HERE
        Debug.Log("HIT");
    }

    public void Fire(int direction) {
        // Find point infront of mech
        float startAngle = 45 * (6 - direction);
        if (direction > 4)
            startAngle = 45 * (direction - 6);

        // This is to get the correct X for hand swipe with sword
        Vector3 xRot = TileManager.rotate(new Vector2(-rotatePoint.x, 0), startAngle);

        startAngle += arcLength;
        duration = (arcLength * 2) / rotateSpeed;

        // Set position to offset Position
        offsetPosition += xRot;
        transform.position = offsetPosition - new Vector3(transform.lossyScale.x/2, 0);

        transform.RotateAround(offsetPosition, Vector3.forward, startAngle);
    }
}
