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
    private GameObject shotgunVFX;

    //private GameObject aimObject1;
    //private GameObject aimObject2;
    private float minAimDegree;
    private float maxAimDegree;
    private float destroyObjectTimer = 0.05f;
    private bool fired = false;

    public void Fire() {
        if (fired)
            return;

        shotgunVFX.SetActive(true);
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

        if(direction.x < 0)
            transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

        transform.Rotate(0.0f, 0.0f, minAimDegree);

        Vector3 offset;

        switch(minAimDegree)
        {
            case 0:
                offset = new Vector3(-0.818f, 0.08f, 0.0f);
                break;
            case 45:
                offset = new Vector3(-0.107f, 0.117f, 0.0f);
                break;
            case 90:
                offset = new Vector3(0.61f, -0.015f, 0.0f);
                break;
            case 135:
                offset = new Vector3(0.847f, 0.114f, 0.0f);
                break;
            case 180:
                offset = new Vector3(0.9f, 0.818f, 0.0f);
                break;
            case -45:
                offset = new Vector3(-0.89f, 0.52f, 0.0f);
                break;
            case -90:
                offset = new Vector3(-0.5f, 1f, 0.0f);
                break;
            case -135:
                offset = new Vector3(0.24f, 1.128f, 0.0f);
                break;
            default:
                offset = TileManager.rotate(offsetPoint, minAimDegree);
                break;
        }

        transform.position = transform.position + (offset * 0.8f);

        minAimDegree -= spreadAngle / 2.0f;
        maxAimDegree = minAimDegree + spreadAngle;
    }

    public int GetSeedCount()
    {
        return seedCount;
    }
}
