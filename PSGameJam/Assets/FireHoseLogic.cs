using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.TextCore;

public class FireHoseLogic : MonoBehaviour
{
    private GameObject aimObject;
    private float minAimDegree;
    private float maxAimDegree;

    [SerializeField]
    private GameObject aimPrefab;

    [SerializeField]
    private GameObject firehosePrefab;

    [SerializeField]
    private float waterSpreadY = 3;

    [SerializeField]
    private float firehoseSpeed;


    public void SprayWater(float chargeTime) {
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
        Destroy(gameObject);
        Vector3 testme = (aimObject.transform.position - transform.position).normalized;
        GameObject testt1;
        testt1 = Instantiate(firehosePrefab);
        testt1.transform.localScale = new Vector3(testt1.transform.localScale.x * scaleWater, waterSpreadY, 0);
        testt1.transform.rotation = aimObject.transform.rotation;
        testt1.transform.position = transform.position + (testme * testt1.transform.lossyScale.x/2);
        //testt1.GetComponent<LaserBeamLogic>().ExpandBeam(0.01f, testme);
        // Get amount of frames it will take to complete
        //duration = (aimObject.transform.lossyScale.x - testt1.transform.lossyScale.x) / 0.01f;

    }
    public void WaterSpread(int direction)
    {
        minAimDegree = 45 * (7 - direction);
        if (direction > 4)
        {
            minAimDegree = 45 * Math.Abs(5 - direction);
        }

        maxAimDegree = minAimDegree - 45;

        aimObject = Instantiate(aimPrefab, transform);
        aimObject.transform.RotateAround(transform.position, Vector3.forward, maxAimDegree);
    }


}

