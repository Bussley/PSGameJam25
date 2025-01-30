using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoilLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject potatoPrefab;
    [SerializeField]
    private GameObject wheatPrefab;
    [SerializeField]
    private GameObject eggplantPrefab;
    [SerializeField]
    private GameObject strawberryPrefab;
    [SerializeField]
    private GameObject blackberryPrefab;
    [SerializeField]
    private GameObject tomatoPrefab;
    [SerializeField]
    private GameObject pepperPrefab;
    [SerializeField]
    private GameObject witheredCrop;

    [SerializeField]
    private Sprite wateredSoil;

    [SerializeField]
    private Sprite drySoil;

    [SerializeField]
    private Sprite charredSoil;

    [SerializeField]
    private float charredTimer;

    [SerializeField]
    private GameObject cropDestroyParticales;

    private GameObject crop;
    private SpriteRenderer spr;
    private float invincibilityTime;

    private bool charred;

    void Awake()
    {
        // This controls charred time timer invicibilty so newly spawn soil doesn't go away
        invincibilityTime = Time.time + 1.0f;
        charred = false;
        spr = GetComponent<SpriteRenderer>();

        WeatherManager.AddRainListener(Watered);

        if(WeatherManager.Raining())
            spr.sprite = wateredSoil;
    }

    public void AddSeed() {
        if (!charred && crop == null)
        {
            switch(SeedLogic.currentSeed)
            {
                case "potato":
                    crop = Instantiate(potatoPrefab, transform);
                    break;
                case "wheat":
                    crop = Instantiate(wheatPrefab, transform);
                    break;
                case "eggplant":
                    crop = Instantiate(eggplantPrefab, transform);
                    break;
                case "strawberry":
                    crop = Instantiate(strawberryPrefab, transform);
                    break;
                case "blackberry":
                    crop = Instantiate(blackberryPrefab, transform);
                    break;
                case "tomato":
                    crop = Instantiate(tomatoPrefab, transform);
                    break;
                case "pepper":
                    crop = Instantiate(pepperPrefab, transform);
                    break;
                default:
                    crop = Instantiate(potatoPrefab, transform);
                    break;
            }
            
        }
    }
    
    public void AddWithered()
    {
        Destroy(crop);
        crop = Instantiate(witheredCrop, transform);
    }

    public void Watered()
    {
        spr.sprite = wateredSoil;
    }

    public void Dry()
    {
        spr.sprite = drySoil;
    }

    public void RemoveCrop()
    {
        if (crop != null && (crop.tag == "WitheredCrop" || !crop.GetComponent<CropLogic>().IsSeed()))
        {
            Instantiate(cropDestroyParticales, crop.transform.position, Quaternion.identity);
            Destroy(crop);
        }
    }

    public void CharTile() {
        // Make sure not to char tile if just spawned
        if(Time.time > invincibilityTime && !charred)
        {
            charred = true;
            if(crop != null)
                Destroy(transform.GetChild(0).gameObject);

            spr.sprite = charredSoil;

            Action func = () =>
            {
                TileManager.ResetTile(transform.position);
            };

            TimerManager.AddTimer(func, charredTimer);
        }

    }

    public bool WetSoil()
    {
        if (spr.sprite == wateredSoil)
            return true;
        else 
            return false;
    }
}
