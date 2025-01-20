using System;
using UnityEngine;

public class CropLogic : MonoBehaviour
{
    [SerializeField]
    private CropScriptableObject cropSO;

    [SerializeField]
    private float dryCropTimer;

    // Grows from 0 to 100, each crop has different stages based on percentage:
    // SEEDED (brand new crop, denotes early life. First 10% of life)
    // YOUNG (younger half of growing. 20-70% of growing)
    // ALMOST-RIPE (denotes that this will soon be ready 70-99% of growing)
    // RIPE (ready to harvest. 100% of growing)
    private int growthPercentage;

    private SpriteRenderer mSprite;

    private float health;

    private bool watered;

    private void Start() {
        growthPercentage = 0;
        health = cropSO.maxHealth;
        mSprite = GetComponent<SpriteRenderer>();
        GrowTask();
    }
    
    private void GrowTask() {
        // Check if crop is still growing
        if (growthPercentage < 100) {
            if (growthPercentage == cropSO.growToYoungCrop) { // Switch from seed to young crop
                mSprite.sprite = cropSO.youngCropSprite;
            }
            else if (growthPercentage == cropSO.growToGrowingCrop) { // Switch from young crop to growing
                mSprite.sprite = cropSO.growingCropSprite;
            }

            Action repeatGrow = () => {
                growthPercentage += 1;
                GrowTask();
            };

            //Debug.Log(growthPercentage);
            TimerManager.AddTimer(repeatGrow, cropSO.growSpeedTime);
        }
        else { // Switch from growing crop to ripe
            mSprite.sprite = cropSO.ripeCropSprite;
        }
    }

    public void WaterCrop() {
        watered = true;

        Action dryCrop = () => {
            watered = false;
        };

        TimerManager.AddTimer(dryCrop, dryCropTimer);
    }
}
