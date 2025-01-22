using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float crowSpawnTimer;

    [SerializeField]
    private GameObject crowPrefab;

    void Awake()
    {
        TimerManager.AddTimer(SpawnCrow, crowSpawnTimer);
    }
    

    void SpawnCrow()
    {
        // Check if there are crops
        GameObject[] cropList = GameObject.FindGameObjectsWithTag("Crop");


        // Get random crop for crow to attack
        if(cropList.Count() > 0)
        {
            Array.Sort(cropList, RanndomSort);

            GameObject crop = null;

            // Find first crop not protected by scarecrow
            foreach (var c in cropList)
            {
                if (!c.GetComponent<CropLogic>().scareCrowProtected)
                {
                    crop = c;
                    break;
                }
            }

            // If crop found spawn crow
            if(crop != null)
            {
                GameObject crow = Instantiate(crowPrefab);
                crow.GetComponent<CrowLogic>().TargetCrop(crop);
            }
        }

        TimerManager.AddTimer(SpawnCrow, crowSpawnTimer);
    }

    int RanndomSort(GameObject a, GameObject b)
    {
        return UnityEngine.Random.Range(- 1, 1);
    }
}
