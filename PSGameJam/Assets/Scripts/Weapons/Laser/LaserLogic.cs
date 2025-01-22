using System;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    [SerializeField]
    private float aimRotateSpeed;
    [SerializeField]
    private float aimStartArc;
    [SerializeField]
    private float laserBeamSpeed;
    [SerializeField]
    private GameObject aimPrefab;
    [SerializeField]
    private GameObject laserPrefab;

    private GameObject aimObject1;
    private GameObject aimObject2;
    private GameObject laserObject1;
    private GameObject laserObject2;
    private float maxAimDuration;
    private float currentAimDuration;
    private float maxLaserBeamDuration;
    private float currentLaserBeamDuration;

    private bool fired;
    private bool aimCreated;

    void Awake() {
        fired = false;
        aimCreated = false;
        currentLaserBeamDuration = 0;
        currentAimDuration = 0;
    }

    void Update()
    {
        // Rotate for a aimers at rotate amount until Fire is called
        if (aimCreated && !fired)
            RotateAim();
        else if(fired)
        {
            if (currentLaserBeamDuration > maxLaserBeamDuration)
            {
                transform.parent.GetComponent<PlayerController>().SetMove(true);
                Destroy(laserObject1);
                Destroy(laserObject2);
                Destroy(gameObject);
            }
            currentLaserBeamDuration++;
        }
    }

    private void RotateAim() {
        aimObject1.transform.RotateAround(transform.position, Vector3.forward, aimRotateSpeed);
        aimObject2.transform.RotateAround(transform.position, Vector3.forward, -aimRotateSpeed);

        if (maxAimDuration < currentAimDuration)
            Fire();

        currentAimDuration++;
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
        laserObject1.GetComponent<LaserBeamLogic>().ExpandBeam(laserBeamSpeed, laser1Dir);

        // Spawn laser 2 and get direction it is heading
        Vector3 laser2Dir = (aimObject2.transform.position - transform.position).normalized;
        laserObject2 = Instantiate(laserPrefab);
        laserObject2.transform.rotation = aimObject2.transform.rotation;
        laserObject2.transform.position = transform.position + (laser2Dir * laserObject2.transform.lossyScale.x/2);
        laserObject2.GetComponent<LaserBeamLogic>().ExpandBeam(laserBeamSpeed, laser2Dir);

        // Get amount of frames it will take to complete
        maxLaserBeamDuration = (aimObject1.transform.lossyScale.x - laserObject1.transform.lossyScale.x) / laserBeamSpeed;

        Destroy(aimObject1);
        Destroy(aimObject2);
    }

    public void Charge(Vector2 direction) {
        //Determine Starting angle
        float angle = Vector2.Angle(Vector2.left, direction);
        
        // Flip if face positive direction
        if(direction.y > 0)
            angle = - angle;

        aimObject1 = Instantiate(aimPrefab, transform);
        aimObject2 = Instantiate(aimPrefab, transform);

        aimObject1.transform.RotateAround(transform.position, Vector3.forward, angle - aimStartArc);
        aimObject2.transform.RotateAround(transform.position, Vector3.forward, angle + aimStartArc);

        maxAimDuration = aimStartArc / aimRotateSpeed;
        aimCreated = true;
    }

    public bool Fired() {
        return fired;
    }
}
