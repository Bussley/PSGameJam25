using System;
using System.Threading.Tasks;
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
    private float duration;
    private float currentDuration;

    private bool fired;
    private bool aimCreated;

    void Awake() {
        fired = false;
        aimCreated = false;
        currentDuration = 0;
    }

    void Update()
    {
        // Rotate for a aimers at rotate amount until Fire is called
        if (aimCreated && !fired)
            RotateAim();
        else if(fired)
        {
            if (currentDuration < duration)
                currentDuration++;
            else
            {
                transform.parent.GetComponent<PlayerController>().SetMove(true);
                Destroy(laserObject1);
                Destroy(laserObject2);
                Destroy(gameObject);
            }
        }
    }

    private float DetermineSpawnAngle(int direction) {
        return Mathf.Abs(4 - direction) * 45.0f;
    }

    private void RotateAim() {
        aimObject1.transform.RotateAround(transform.position, Vector3.forward, rotateAmount);
        aimObject2.transform.RotateAround(transform.position, Vector3.forward, -rotateAmount);
    }

    // Return duration to let player move again after lasers are done
    public void Fire() {
        //Destroy aimobject and fire red laser
        fired = true;

        // Spawn laser 1 and get direction it is heading
        Vector3 laser1Dir = (aimObject1.transform.position - transform.position).normalized;
        laserObject1 = Instantiate(laserPrefab);
        laserObject1.transform.rotation = aimObject1.transform.rotation;
        laserObject1.transform.position = transform.position + (laser1Dir * laserObject1.transform.lossyScale.x/2);
        laserObject1.GetComponent<LaserBeamLogic>().ExpandBeam(laserSpeed, laser1Dir);

        // Spawn laser 2 and get direction it is heading
        Vector3 laser2Dir = (aimObject2.transform.position - transform.position).normalized;
        laserObject2 = Instantiate(laserPrefab);
        laserObject2.transform.rotation = aimObject2.transform.rotation;
        laserObject2.transform.position = transform.position + (laser2Dir * laserObject2.transform.lossyScale.x/2);
        laserObject2.GetComponent<LaserBeamLogic>().ExpandBeam(laserSpeed, laser2Dir);

        // Get amount of frames it will take to complete
        duration = (aimObject1.transform.lossyScale.x - laserObject1.transform.lossyScale.x) / laserSpeed;

        Destroy(aimObject1);
        Destroy(aimObject2);
    }

    public void Charge(int direction) {
        //Determine Starting angle
        float angle = DetermineSpawnAngle(direction);
        Debug.Log(angle);

        aimObject1 = Instantiate(aimPrefab, transform);
        aimObject2 = Instantiate(aimPrefab, transform);

        // Need to flip when facing East to rotate correctly
        if (direction > 4) {

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
