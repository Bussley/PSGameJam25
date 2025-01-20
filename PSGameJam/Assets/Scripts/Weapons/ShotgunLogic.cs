using System;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShotgunLogic : MonoBehaviour
{
    [SerializeField]
    private float rotateAmount;

    [SerializeField]
    private float randomMinRange;

    [SerializeField]
    private float randomMaxRange;
    [SerializeField]
    private float shotgunSpeed;
    [SerializeField]
    private GameObject aimPrefab;
    [SerializeField]
    private GameObject shotgunPrefab;

    private GameObject aimObject1;
    private GameObject aimObject2;
    private Vector2 shotgun1Destination;
    private bool fired;
    private bool aimCreated;

    void Awake() {
        fired = false;
        aimCreated = false;
    }

    private void PlotLand(Vector2 pos)
    {
        TileManager.ChangeTile(pos);
    }

    public void Fire(int direction) {
        //Destroy aimobject and fire red shotgun
        fired = true;
        
        for (int i = 0; i<= 3; i++){
            var min = 45 * (7- direction);
            if (direction > 4) {
                min = 45 * Math.Abs(5 - direction);
            }

            var max = min - 90;

            aimObject1 = Instantiate(aimPrefab, transform);
            aimObject2 = Instantiate(aimPrefab, transform);
            aimObject1.transform.RotateAround(transform.position, Vector3.forward, UnityEngine.Random.Range(min,max));
            aimObject2.transform.RotateAround(transform.position, Vector3.forward, UnityEngine.Random.Range(min,max));
            GameObject testme;
            // Spawn shotgun 1 and get direction it is heading
            shotgun1Destination = aimObject1.transform.position - transform.position;
            Vector2 shotgunDir = shotgun1Destination.normalized;
            testme = Instantiate(shotgunPrefab);
            testme.transform.position = transform.position;
            testme.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shotgunDir.x * shotgunSpeed, shotgunDir.y * shotgunSpeed);

             float seedTime = UnityEngine.Random.Range(randomMinRange,randomMaxRange);
            testme.GetComponent<ShotgunSeedLogic>().RemoveShotgunBullet(seedTime);
        }
       Destroy(gameObject);
    }
}
