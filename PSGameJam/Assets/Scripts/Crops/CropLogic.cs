using System;
using UnityEngine;

public class CropLogic : MonoBehaviour
{
    [SerializeField]
    private CropScriptableObject cropSO;

    // Grows from 0 to 100, each crop has different stages based on percentage:
    // SEEDED (brand new crop, denotes early life. First 10% of life)
    // YOUNG (younger half of growing. 20-70% of growing)
    // ALMOST-RIPE (denotes that this will soon be ready 70-99% of growing)
    // RIPE (ready to harvest. 100% of growing)
    private int growthPercentage;

    private SpriteRenderer mSprite;

    private float health;

    private float hydrationLevel;

    public bool scareCrowProtected { get; set; }

    private void Start() {
        scareCrowProtected = false;
        growthPercentage = 0;
        health = cropSO.maxHealth;
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.sprite = cropSO.seedSprite;
        GrowTask();
        Dehydrate();
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
                if(hydrationLevel > 0)
                    growthPercentage += 1;
                GrowTask();
            };

            // Repeat this task
            TimerManager.AddTimer(repeatGrow, cropSO.growSpeedTime);
        }
        else { // Switch from growing crop to ripe
            mSprite.sprite = cropSO.ripeCropSprite;
        }
    }

    private void Dehydrate() {
        // Maybe move implmentation to soil logic
        // Crops die if hydration level is 0?
        if(hydrationLevel == 0) {
            transform.parent.GetComponent<SoilLogic>().Dry();
            
            //Just so it doesn't keep counting down for now
            hydrationLevel -= 1;
        }
        else if (hydrationLevel > 1)
            hydrationLevel -= 1;

        Action repeatDehydration = () =>
        {
            Dehydrate();
        };

        TimerManager.AddTimer(repeatDehydration, CropScriptableObject.DehydrationSpeedTime);
    }

    public void WaterCrop() {
        hydrationLevel = 100;
        transform.parent.GetComponent<SoilLogic>().Watered();
    }
}
