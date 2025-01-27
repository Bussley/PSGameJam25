using System;
using System.Security.Authentication;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CropLogic : MonoBehaviour
{
    [SerializeField]
    private CropScriptableObject cropSO;

    [SerializeField]

    private LayerMask hitMask;

    // Grows from 0 to 100, each crop has different stages based on percentage:
    // CropED (brand new crop, denotes early life. First 10% of life)
    // YOUNG (younger half of growing. 20-70% of growing)
    // ALMOST-RIPE (denotes that this will soon be ready 70-99% of growing)
    // RIPE (ready to harvest. 100% of growing)
    private int growthPercentage;

    private SpriteRenderer mSprite;

    private float hydrationLevel;

    public bool scareCrowProtected { get; set; }
    public bool targeted { get; set; }

    private bool cold;

    private Bounds collisionBounds;

    private bool attacked;

    private float attackedTime;
    private float freezeTime;

    private void Awake() {
        name = cropSO.cropType;

        targeted = false;
        attacked = false;
        cold = false;
        scareCrowProtected = false;
        growthPercentage = 0;
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.sprite = cropSO.seedSprite;
        collisionBounds = GetComponent<PolygonCollider2D>().bounds;

        if (transform.parent.GetComponent<SoilLogic>().WetSoil())
        {
            hydrationLevel = 100;
        }

        GrowTask();
        Dehydrate();
    }

    private void FixedUpdate()
    {
        if(attacked && attackedTime < Time.time)
        {
            transform.parent.gameObject.GetComponent<SoilLogic>().RemoveCrop();
        }

        if (cold)
        {
            Collider2D hits = Physics2D.OverlapBox(collisionBounds.center, collisionBounds.extents, 0.0f, hitMask);

            if (hits != null)
            {
                if (freezeTime < Time.time)
                {
                    // Spawn withered crop object
                    transform.parent.gameObject.GetComponent<SoilLogic>().RemoveCrop();
                }
            }
            else
            {
                cold = false;
            }
        }
    }

    private void Update()
    {
        if(WeatherManager.Raining())
        {
            hydrationLevel = 100;
        }
    }

    private void GrowTask() {
        if (this == null)
            return;

        // Check if crop is still growing
        if ( growthPercentage < 100) {
            if (growthPercentage == cropSO.growToYoungCrop) { // Switch from Crop to young crop
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

    public void CrowOnCrop(bool onCrop)
    {
        if(onCrop)
        {
            attackedTime = Time.time + cropSO.cropCrowDurabilityTime;
            attacked = true;
        }
        else
            attacked = false;
    }

    public bool IsSeed()
    {
        if(growthPercentage < cropSO.growToYoungCrop)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<PlayerController>().GetUsingJets())
        {
            transform.parent.gameObject.GetComponent<SoilLogic>().RemoveCrop();
        }
        
        if(collision.gameObject.tag == "SnowPowder" && !cold)
        {
            freezeTime = Time.time + cropSO.cropFreezeDurabilityTime;
            cold = true;
        }
    }
    // Types of Crop Logic

    /*
    [SerializeField]
    private int tomatoCount = 0;
    [SerializeField]
    private int wheatCount = 25;
    [SerializeField]
    private int potatoCount = 0;
    [SerializeField]
    private int pepperCount = 0;
    [SerializeField]
    private int strawBerryCount = 0;

    [SerializeField]    
    public String[] typesOfCrops = {
        "wheat", // 0
        "tomato", // 1
        "pepper", // 2
        "strawberry", // 3
        "potato", // 4
    };

    // Havisting crops yeald 1 of that crop. and randomize 1-3 seeds.
    public void HarvestCrop(String CropType) {
        switch (CropType) {
            case "wheat":
                wheatCount += 1;
                break;
            case "tomato":
                tomatoCount += 1;
                break;
            case "pepper":
                pepperCount += 1;
                break;
            case "strawberry":
                strawBerryCount += 1;
                break;
            case "potato":
                potatoCount += 1;
                break;                                                
        }
    }


    public int GetCropCount(String CropType) {
        switch (CropType) {
            case "wheat":
                return wheatCount;
            case "tomato":
                return tomatoCount;
            case "pepper":
                return pepperCount;
            case "strawberry":
                return strawBerryCount;
            case "potato":
                return potatoCount;
            default:
                return 0;
        }
    }
    */
}
