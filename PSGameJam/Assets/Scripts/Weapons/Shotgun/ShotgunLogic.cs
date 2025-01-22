using System;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
    private GameObject aimPrefab;
    [SerializeField]
    private GameObject shotgunPrefab;

    private GameObject aimObject1;
    private GameObject aimObject2;
    private float minAimDegree;
    private float maxAimDegree;

    public void Fire() {
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
        Destroy(gameObject);
    }

    public void BulletSpread(Vector2 direction)
    {
        minAimDegree = Vector2.Angle(Vector2.left, direction);

        if(direction.y > 0)
            minAimDegree = -minAimDegree;

        minAimDegree -= spreadAngle / 2.0f;
        maxAimDegree = minAimDegree + spreadAngle;

        aimObject1 = Instantiate(aimPrefab, transform);
        aimObject2 = Instantiate(aimPrefab, transform);
        aimObject1.transform.RotateAround(transform.position, Vector3.forward, minAimDegree);
        aimObject2.transform.RotateAround(transform.position, Vector3.forward, maxAimDegree);
    }
}
