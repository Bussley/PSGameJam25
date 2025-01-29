using System;
using UnityEngine;
using UnityEngine.Animations;

public class HarvestBladeLogic : MonoBehaviour
{
    [SerializeField] 
    private int seedMin;

    [SerializeField] 
    private int seeMax;

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

    private BountyLogic bountyLogic;

    private GameObject bountyObj;


    private void Awake() {
        playerLogic = transform.parent.GetComponent<PlayerController>();
        bountyObj = GameObject.FindGameObjectWithTag("CropMarketBoard");
        bountyLogic = bountyObj.GetComponent<BountyLogic>();
        curTime = 0;
        seedMin = 1;
        seeMax = 5;
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
        if (collision.gameObject.tag == "Crop" && !collision.gameObject.GetComponent<CropLogic>().IsSeed() && collision.gameObject.GetComponent<CropLogic>().IsGrown()) {
            playerLogic.seeds.SeedLevel(UnityEngine.Random.Range(seedMin, seeMax),collision.gameObject.name);

            playerLogic.wallet += CropMarketPlaceLogic.GetCropPrice(collision.gameObject.name);

            Debug.Log(bountyLogic.bountyCrop + "test");
            if (collision.gameObject.name == bountyLogic.bountyCrop && bountyLogic.bountyStart) {
                bountyLogic.bountyCropCount += 1;
            }
            Debug.Log(playerLogic.wallet);

            collision.gameObject.transform.parent.GetComponent<SoilLogic>().RemoveCrop();
        }
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
        transform.position = offsetPosition;
    }
}
