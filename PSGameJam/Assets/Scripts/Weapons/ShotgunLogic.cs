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
    private GameObject lazerPrefab;

    private GameObject aimObject1;
    private GameObject aimObject2;
    private GameObject shotgunObject1;
    private GameObject shotgunObject2;
    private Vector2 shotgun1Destination;
    private Vector2 shotgun2Destination;

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

    private float DetermineSpawnAngle(int direction) {
        return Mathf.Abs(4 - direction) * 45.0f;
    }

    private void RotateAim() {
        aimObject1.transform.RotateAround(transform.position, Vector3.forward, rotateAmount);
        aimObject2.transform.RotateAround(transform.position, Vector3.forward, -rotateAmount);
    }

    private void PlotLand(Vector2 pos)
    {
        TileManager.ChangeTile(pos);
    }

    public void Fire() {
        //Destroy aimobject and fire red shotgun
        fired = true;
        Vector3 mousePos = Input.mousePosition;

        // Spawn shotgun 1 and get direction it is heading
        shotgun1Destination = aimObject1.transform.position - transform.position;
        Vector2 shotgun1Dir = shotgun1Destination.normalized;
        shotgunObject1 = Instantiate(lazerPrefab);
        shotgunObject1.transform.position = transform.position;
        shotgunObject1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(mousePos.x * shotgunSpeed, mousePos.y * shotgunSpeed);

        // Spawn shotgun 2 and get direction it is heading
        shotgun2Destination = aimObject2.transform.position - transform.position;
        Vector2 shotgun2Dir = shotgun2Destination.normalized;
        shotgunObject2 = Instantiate(lazerPrefab);
        shotgunObject2.transform.position = transform.position;
        shotgunObject2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2((mousePos.x + 1) * shotgunSpeed, (mousePos.x + 2) * shotgunSpeed);

        float dur = aimObject1.transform.lossyScale.x * shotgunSpeed;

        Destroy(aimObject1);
        Destroy(aimObject2);

        Action shotgunToLandFunc = () =>
        {
            PlotLand(shotgunObject1.transform.position);
            PlotLand(shotgunObject2.transform.position);
            Destroy(shotgunObject1);
            Destroy(shotgunObject2);
        };

        // Create timer for shotguns plotting the land
        TimerManager.AddTimer(shotgunToLandFunc, dur);
        Destroy(gameObject);
    }

    public void Charge(int direction) {
        //Determine Starting angle
        float angle = DetermineSpawnAngle(direction);
        Debug.Log(angle);

        aimObject1 = Instantiate(aimPrefab, transform);
        aimObject2 = Instantiate(aimPrefab, transform);

        // Need to flip when facing East to rotate correctly
        if(direction > 4) {

            aimObject1.transform.RotateAround(transform.position, Vector3.forward, angle + 180.0f);
            aimObject2.transform.RotateAround(transform.position, Vector3.forward, angle);
        }
        else {
            aimObject1.transform.RotateAround(transform.position, Vector3.forward, angle);
            aimObject2.transform.RotateAround(transform.position, Vector3.forward, angle + 180.0f);
        }

        aimCreated = true;
    }
}
