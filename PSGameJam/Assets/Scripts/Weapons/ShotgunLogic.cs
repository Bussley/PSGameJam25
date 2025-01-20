using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShotgunLogic : MonoBehaviour
{
    [SerializeField]
    private float rotateAmount;
    [SerializeField]
    private float shotgunSpeed;
    [SerializeField]
    private GameObject aimPrefab;
    [SerializeField]
    private GameObject shotgunPrefab;

    private GameObject aimObject1;
    private GameObject aimObject2;
    private GameObject shotgunObject1;
    private GameObject shotgunObject2;
    private GameObject shotgunObject3;
    private GameObject shotgunObject4;
    private GameObject shotgunObject5;
    private Vector2 shotgun1Destination;
    private Vector2 shotgun2Destination;
    private Vector2 shotgun3Destination;
    private bool fired;
    private bool aimCreated;

    void Awake() {
        fired = false;
        aimCreated = false;
    }

    void Update()
    {
        // Rotate for a aimers at rotate amount until Fire is called
        if (aimCreated && !fired)
            RotateAim();
    }
    private void RotateAim() {
        aimObject1.transform.RotateAround(transform.position, Vector3.forward, rotateAmount);
    }

    private void PlotLand(Vector2 pos)
    {
        TileManager.ChangeTile(pos);
    }

    public void Fire() {
        //Destroy aimobject and fire red shotgun
        fired = true;
        

        // Spawn shotgun 1 and get direction it is heading
        shotgun1Destination = aimObject1.transform.position - transform.position;
        Vector2 shotgun1Dir = shotgun1Destination.normalized;
        shotgunObject1 = Instantiate(shotgunPrefab);
        shotgunObject1.transform.position = transform.position;
        shotgunObject1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shotgun1Dir.x * shotgunSpeed, shotgun1Dir.y * shotgunSpeed);
        Action shotgunToLandFunc = () =>
        {
            PlotLand(shotgunObject1.transform.position);
            Destroy(shotgunObject1);
        };
        float dur = aimObject1.transform.lossyScale.x * shotgunSpeed;
        // Create timer for shotguns plotting the land
        TimerManager.AddTimer(shotgunToLandFunc, dur);

        // Spawn shotgun 2 and get direction it is heading
        shotgun2Destination = aimObject2.transform.position - transform.position;
        Vector2 shotgun2Dir = shotgun2Destination.normalized;
        shotgunObject2 = Instantiate(shotgunPrefab);
        shotgunObject2.transform.position = transform.position;
        shotgunObject2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shotgun2Dir.x * shotgunSpeed, shotgun2Dir.y * shotgunSpeed);
        Action shotgunToLandFunc2 = () =>
        {
            PlotLand(shotgunObject2.transform.position);
            Destroy(shotgunObject2);
        };
        float dur2 = aimObject2.transform.lossyScale.x * shotgunSpeed * 1.5f;
        // Create timer for shotguns plotting the land
        TimerManager.AddTimer(shotgunToLandFunc2, dur2);

        // Spawn shotgun 3 and get direction it is heading
        shotgun3Destination = aimObject1.transform.position - transform.position;
        Vector2 shotgun3Dir = shotgun3Destination.normalized;
        shotgunObject3 = Instantiate(shotgunPrefab);
        shotgunObject3.transform.position = transform.position;
        shotgunObject3.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shotgun3Dir.x * shotgunSpeed, shotgun3Dir.y * shotgunSpeed);
        Destroy(aimObject1);
        Action shotgunToLandFunc3 = () =>
        {
            PlotLand(shotgunObject3.transform.position);
            Destroy(shotgunObject3);
        };
        float dur3 = aimObject1.transform.lossyScale.x * shotgunSpeed * 1.5f;
        // Create timer for shotguns plotting the land
        TimerManager.AddTimer(shotgunToLandFunc3, dur3);
        
        Destroy(gameObject);
    }

    public void Charge(int direction) {
        //Determine Starting angle

        // N = 0, NE = 1, E = 2, SE = 3
        // S = 4, NW = 5, W = 6, SW = 7

        aimObject1 = Instantiate(aimPrefab, transform);
        aimObject2 = Instantiate(aimPrefab, transform);
        Vector3 mousePos = Input.mousePosition;
        // Need to flip when facing East to rotate correctly
        switch (direction)
        {
            case 0:
                // angle for N 270.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 270.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 275.0f);

            break;
            case 1:
                // angle for NE 225.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 225.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 230.0f);
            break;
            case 2:
                // angle for E 180.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 180.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 185.0f);
            break;
            case 3:
                // angle for E 135.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 135.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 140.0f);
            break;
            case 4:
                // angle for E 90.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 90.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 95.0f);
            break;
            case 5:
                // angle for E 315.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 315.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 320.0f);
            break;
            case 6:
                 // angle for E 0.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 0.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 5.0f);
            break;
            case 7:
                // angle for E 45.0f
                aimObject1.transform.RotateAround(transform.position, Vector3.forward, 45.0f);
                aimObject2.transform.RotateAround(transform.position, Vector3.forward, 50.0f);
            break;            
        }

        aimCreated = true;
    }
}
