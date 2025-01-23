using System;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotgunLogic : MonoBehaviour
{
    [SerializeField]
    private float randomMinRange;
    [SerializeField]
    private float randomMaxRange;
    [SerializeField]
    private float shotgunSpeed;
    [SerializeField]
    private int seedCount;
    [SerializeField] 
    private float spreadAngle;
    [SerializeField]
    private Vector2 offsetPoint;
    //[SerializeField]
    //private GameObject aimPrefab;
    [SerializeField]
    private GameObject shotgunPrefab;
    [SerializeField]
    private GameObject shotgunVFXPrefab;

    //private GameObject aimObject1;
    //private GameObject aimObject2;
    private float minAimDegree;
    private float maxAimDegree;
    private float destroyObjectTimer = 0.5f;
    private bool fired = false;

    public void Fire() {
        if (fired)
            return;

        //Destroy(aimObject1);
        //Destroy(aimObject2);

        Instantiate(shotgunVFXPrefab, transform);
        for (int i = 0; i< seedCount; i++)
        {
            var ran = UnityEngine.Random.Range(minAimDegree, maxAimDegree);
            GameObject bullet;
            // Get pellete direction based on rotation
            Vector2 shotgunDir = TileManager.rotate(Vector2.left, ran).normalized;
            bullet = Instantiate(shotgunPrefab);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(shotgunDir.x * shotgunSpeed, shotgunDir.y * shotgunSpeed);

            float seedTime = UnityEngine.Random.Range(randomMinRange,randomMaxRange);
            bullet.GetComponent<ShotgunSeedLogic>().RemoveShotgunBullet(seedTime);
        }

        Action func = () =>
        {
            Destroy(gameObject);
        };

        fired = true;
        TimerManager.AddTimer(func, destroyObjectTimer);
    }

    public void BulletSpread(Vector2 direction)
    {
        minAimDegree = Vector2.Angle(Vector2.left, direction);

        if(direction.y > 0)
            minAimDegree = -minAimDegree;

        transform.Rotate(0.0f, 0.0f, minAimDegree);

        Vector3 rot_offset = TileManager.rotate(offsetPoint, minAimDegree);
        transform.position = transform.position + rot_offset;

        minAimDegree -= spreadAngle / 2.0f;
        maxAimDegree = minAimDegree + spreadAngle;

        /*
        aimObject1 = Instantiate(aimPrefab, transform);
        aimObject2 = Instantiate(aimPrefab, transform);
        aimObject1.transform.RotateAround(transform.position, Vector3.forward, -spreadAngle / 2.0f);
        aimObject2.transform.RotateAround(transform.position, Vector3.forward, spreadAngle / 2.0f);
        */
    }
}
