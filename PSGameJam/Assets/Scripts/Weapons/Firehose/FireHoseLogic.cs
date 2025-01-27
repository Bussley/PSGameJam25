using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.TextCore;
using UnityEngine.VFX;

public class FireHoseLogic : MonoBehaviour
{
    private GameObject aimObject;

    [SerializeField]
    private GameObject aimPrefab;

    [SerializeField]
    private GameObject firehosePrefab;

    [SerializeField]
    private float waterSpreadY = 3;

    [SerializeField]
    private float firehoseSpeed;

    [SerializeField]
    private float waterShootTime;

    [SerializeField]
    private GameObject fireHoseVFX;


    public float SprayWater(float chargeTime) {
        Debug.Log("Time charging: " +chargeTime);
        float scaleWater = 1.0f;
        
        if (chargeTime < 3.0f && chargeTime > 2.0f) {
            scaleWater = 2.0f;
        } else if (chargeTime < 5.0f && chargeTime > 3.0f) {
            scaleWater = 3.0f;
        } else if (chargeTime > 5.0f) {
            Debug.Log("EXPLOSION OF WHATER!");
            scaleWater = 0.0f;
            waterSpreadY = 0.0f;
        }
        Vector3 dir = (aimObject.transform.position - transform.position).normalized;

        GameObject firehose = Instantiate(firehosePrefab);
        GameObject firehose_vfx = Instantiate(fireHoseVFX);

        firehose.transform.localScale = new Vector3(firehose.transform.localScale.x * scaleWater, waterSpreadY, 0);
        firehose_vfx.GetComponent<VisualEffect>().SetFloat("Max Speed", scaleWater);

        firehose.transform.rotation = aimObject.transform.rotation;
        firehose_vfx.transform.rotation = aimObject.transform.rotation;

        firehose.transform.position = transform.position + (dir * firehose.transform.lossyScale.x/2);
        firehose_vfx.transform.position = transform.position + (dir * firehose_vfx.transform.lossyScale.x / 2);

        firehose.GetComponent<waterPumpLogic>().DestoryWaterCannon(waterShootTime);

        Action stop_vfx = () =>
        {
            firehose_vfx.GetComponent<VisualEffect>().Stop();
        };
        TimerManager.AddTimer(stop_vfx, waterShootTime);
        Action destroy_vfx = () =>
        {
            Destroy(firehose_vfx);
        };
        TimerManager.AddTimer(destroy_vfx, waterShootTime + 0.5f);

        Destroy(aimObject);
        Destroy(gameObject);
        return waterShootTime;
    }
    
    public void WaterSpread(Vector2 direction)
    {
        float angle = Vector2.Angle(Vector2.left, direction);

        // Flip if face positive direction
        if (direction.y > 0)
            angle = -angle;

        aimObject = Instantiate(aimPrefab, transform);
        aimObject.transform.RotateAround(transform.position, Vector3.forward, angle);
    }

}

