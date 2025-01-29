using System;
using UnityEngine;

public class waterPumpLogic : MonoBehaviour
{
    public void DestoryWaterCannon(float time) {

        Action shotgunToLandFunc = () =>
        {
            Destroy(gameObject);
        };
        TimerManager.AddTimer(shotgunToLandFunc, time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // HIT A CROP TO HARVEST. LOGIC TO CALL CROP.HARVEST HERE
		// NOW WATERS TILLED SOIL
        if(collision.gameObject.tag == "Crop")
		{
            collision.gameObject.GetComponent<CropLogic>().WaterCrop();
		}
		if(collision.gameObject.tag == "Soil")
		{
			collision.gameObject.GetComponent<SoilLogic>().Watered();
		}
    }
}
