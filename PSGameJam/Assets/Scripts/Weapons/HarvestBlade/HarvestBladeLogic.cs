using UnityEngine;
using UnityEngine.Animations;

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


    private PlayerController playerLogic;

    private GameObject playerObj;


    private void Awake() {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerLogic = playerObj.GetComponent<PlayerController>();
        curTime = 0;
    }

    private void FixedUpdate() {
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
        Debug.Log(collision.gameObject.name);
        playerLogic.seeds.SeedLevel(UnityEngine.Random.Range(1, 3),collision.gameObject.name);
        playerLogic.cLogic.HarvestCrop(playerLogic.cLogic.harvestAmount, collision.gameObject.name);
        playerLogic.wallet += playerLogic.cLogic.ExchangeCrop(collision.gameObject.name);
        Debug.Log(playerLogic.wallet);
        // HIT A CROP TO HARVEST. LOGIC TO CALL CROP.HARVEST HERE
        Debug.Log("HIT");
    }

    public void Fire(Vector2 direction) {
        float angle = Vector2.Angle(Vector2.left, direction);

        // Flip if face positive direction
        if (direction.y > 0)
            angle = -angle;

        // This is to get the correct X for hand swipe with sword
        angle += arcLength;
        transform.Rotate(0.0f, 0.0f, angle);
        offsetPosition = transform.position + (Vector3)TileManager.rotate(rotatePoint, angle);
        duration = (arcLength * 2) / rotateSpeed;
        // Set position to offset Position
        transform.position = offsetPosition - (Vector3)TileManager.rotate(transform.lossyScale / 2, angle);
    }
}
