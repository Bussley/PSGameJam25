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
    private float minAimDegree;
    private float maxAimDegree;

    public void Fire() {
        for (int i = 0; i<= 3; i++)
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

    public void BulletSpread(int direction)
    {
        minAimDegree = 45 * (7 - direction);
        if (direction > 4)
        {
            minAimDegree = 45 * Math.Abs(5 - direction);
        }

        maxAimDegree = minAimDegree - 90;

        aimObject1 = Instantiate(aimPrefab, transform);
        aimObject2 = Instantiate(aimPrefab, transform);
        aimObject1.transform.RotateAround(transform.position, Vector3.forward, minAimDegree);
        aimObject2.transform.RotateAround(transform.position, Vector3.forward, maxAimDegree);
    }
}
