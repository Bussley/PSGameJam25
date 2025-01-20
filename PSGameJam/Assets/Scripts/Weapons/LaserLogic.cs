using System;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    [SerializeField]
    private float rotateAmount;
    [SerializeField]
    private float laserSpeed;
    [SerializeField]
    private GameObject aimPrefab;
    [SerializeField]
    private GameObject laserPrefab;

    private GameObject aimObject1;
    private GameObject aimObject2;
    private GameObject laserObject1;
    private GameObject laserObject2;

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
        //Destroy aimobject and fire red laser
        fired = true;

        // Spawn laser 1 and get direction it is heading
        Vector2 laser1Dir = (aimObject1.transform.position - transform.position).normalized;
        laserObject1 = Instantiate(laserPrefab);
        laserObject1.transform.position = transform.position;
        laserObject1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(laser1Dir.x * laserSpeed, laser1Dir.y * laserSpeed);

        // Spawn laser 2 and get direction it is heading
        Vector2 laser2Dir = (aimObject2.transform.position - transform.position).normalized;
        laserObject2 = Instantiate(laserPrefab);
        laserObject2.transform.position = transform.position;
        laserObject2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(laser2Dir.x * laserSpeed, laser2Dir.y * laserSpeed);

        float dur = aimObject1.transform.lossyScale.x * laserSpeed;

        Destroy(aimObject1);
        Destroy(aimObject2);

        Action laserToLandFunc = () =>
        {
            PlotLand(laserObject1.transform.position);
            PlotLand(laserObject2.transform.position);
            Destroy(laserObject1);
            Destroy(laserObject2);
        };

        // Create timer for lasers plotting the land
        TimerManager.AddTimer(laserToLandFunc, dur);
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
