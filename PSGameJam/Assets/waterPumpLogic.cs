using UnityEngine;

public class waterPumpLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // HIT A CROP TO HARVEST. LOGIC TO CALL CROP.HARVEST HERE
        Debug.Log("WE ARE WATERING OBJECTS!!");
    }
}
