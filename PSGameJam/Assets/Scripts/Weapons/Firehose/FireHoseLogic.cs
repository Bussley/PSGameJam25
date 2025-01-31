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
    private float waterShootTime;

    [SerializeField]
    private GameObject fireHoseVFX;


    public float SprayWater(float chargeTime) {
        Debug.Log("Time charging: " +chargeTime);
        float scaleWater = 1.0f;
        //EDITED THE CHARGE TIMES TO BE MORE FORGIVING AND QUICK -RCCOLA
        if (chargeTime <= 2.0f && chargeTime > 1.0f) {
            scaleWater = 2.0f;
        } else if (chargeTime <= 6.0f && chargeTime > 2.0f) {
            scaleWater = 3.0f;
        } else if (chargeTime > 6.0f)
        {
            Destroy(gameObject);
			//play burst noise
            return 0.0f;
        }
        Debug.Log("Scale Water: " + scaleWater);
        Vector3 dir = (aimObject.transform.position - transform.position).normalized;

        GameObject firehose = Instantiate(firehosePrefab);
        fireHoseVFX.SetActive(true);

        firehose.transform.localScale = new Vector3(firehose.transform.localScale.x * scaleWater, waterSpreadY, 0);
        fireHoseVFX.GetComponent<VisualEffect>().SetFloat("MinSpeed", 2 * scaleWater);
        fireHoseVFX.GetComponent<VisualEffect>().SetFloat("Max Speed", 3 * scaleWater);

        firehose.transform.rotation = aimObject.transform.rotation;

        firehose.transform.position = transform.position + (dir) + (dir * firehose.transform.lossyScale.x / 2.0f);

        firehose.GetComponent<waterPumpLogic>().DestoryWaterCannon(waterShootTime);

        Action stop_vfx = () =>
        {
            Destroy(gameObject);
        };
        TimerManager.AddTimer(stop_vfx, waterShootTime);

        Destroy(aimObject);
        return waterShootTime;
    }
    
    public void WaterSpread(Vector2 direction)
    {
        float angle = Vector2.Angle(Vector2.left, direction);

        // Flip if face positive direction
        if (direction.y > 0)
            angle = -angle;

        if (direction.x < 0)
            transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);

        transform.Rotate(0.0f, 0.0f, angle);

        Vector3 offset;

        switch (angle)
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
                offset = TileManager.rotate(new Vector3(0.586f, 0.471f, 0.0f), angle);
                break;
        }

        aimObject = Instantiate(aimPrefab, transform);
        transform.position = transform.position + (offset * 0.8f);
    }

}

