using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float crowSpawnTimerMin;
    [SerializeField]
    private float crowSpawnTimerMax;

    [SerializeField]
    private GameObject crowPrefab;

    [SerializeField]
    private GameObject scareCrowPrefab;

    void Awake()
    {
        TimerManager.AddTimer(SpawnCrow, UnityEngine.Random.Range(crowSpawnTimerMin, crowSpawnTimerMax));
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
                CropLogic cropLogic = c.GetComponent<CropLogic>();
                if (!cropLogic.scareCrowProtected && !cropLogic.targeted)
                {
                    cropLogic.targeted = true;
                    crop = c;
                    break;
                }
            }

            // If crop found spawn crow
            if(crop != null)
            {
                int i = UnityEngine.Random.Range(0, 2);
                GameObject crow;
                if (i == 0)
                {
                    crow = Instantiate(crowPrefab);
                    crow.transform.position = new Vector3(-crow.transform.position.x, crow.transform.position.y, 0.0f);
                    crow.transform.localScale = Vector3.one;
                }
                else
                    crow = Instantiate(crowPrefab);

                crow.GetComponent<CrowLogic>().TargetCrop(crop);
            }
        }

        TimerManager.AddTimer(SpawnCrow, UnityEngine.Random.Range(crowSpawnTimerMin, crowSpawnTimerMax));
    }

    int RanndomSort(GameObject a, GameObject b)
    {
        return UnityEngine.Random.Range(- 1, 1);
    }

    public void SpawnScarecrow()
    {
        Vector3 spawn = TileManager.GetSpawnableRandomPosition();

        if (spawn == Vector3.one) return;

        // Find a random place to spawn scare crow
        Instantiate(scareCrowPrefab, spawn, new Quaternion());
    }
}
