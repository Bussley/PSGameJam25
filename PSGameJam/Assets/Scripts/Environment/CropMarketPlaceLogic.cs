using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CropMarketPlaceLogic : MonoBehaviour
{
    [SerializeField]
    private float minMarketPercentValue;
    [SerializeField]
    private float maxMarketPercentValue;

    // Wheat, Potato, Tomato, Strawberry, Blackberry, Pepper, Eggplant
    [SerializeField]
    private List<Sprite> UICropImage;

    private GameObject marketPopUpBoard;
    private Image uiCropHigh;
    private Image uiCropLow;

    private Dictionary<String, int> baseCropMarketPrices = new Dictionary<string, int>();
    private static Dictionary<String, int> cropMarketPrices = new Dictionary<string, int>();

    private String lowCrop;
    private String highCrop;

    public Dictionary<String, int> GetCropMarketPrices() {
        return cropMarketPrices;
    }

    void Awake()
    {
        marketPopUpBoard = GameObject.FindGameObjectWithTag("UIStalkMarket");
        uiCropHigh = GameObject.FindGameObjectWithTag("UIHighCrop").GetComponent<Image>();
        uiCropLow = GameObject.FindGameObjectWithTag("UILowCrop").GetComponent<Image>();
        marketPopUpBoard.SetActive(false);

        var allcrops = Resources.LoadAll<CropScriptableObject>("ScriptableObjects/Crops/");

        foreach (CropScriptableObject obj in allcrops)
        {
            baseCropMarketPrices.Add(obj.cropType, obj.baseMarketValue);
            cropMarketPrices.Add(obj.cropType, obj.baseMarketValue);
        }
        RollMarket();
    }

    public void RollMarket()
    {
        int ran1 = UnityEngine.Random.Range(0, baseCropMarketPrices.Count);
        int ran2;
        do
        {
            ran2 = UnityEngine.Random.Range(0, baseCropMarketPrices.Count);
        }
        while (ran1 == ran2);


        int i = 0;

        foreach (var c in baseCropMarketPrices)
        {
            if (i == ran1)
            {
                highCrop = c.Key;
                cropMarketPrices[c.Key] = (int)(baseCropMarketPrices[c.Key] * UnityEngine.Random.Range(1, maxMarketPercentValue));
            }
            else if (i == ran2)
            {
                lowCrop = c.Key;
                cropMarketPrices[c.Key] = (int)(baseCropMarketPrices[c.Key] * UnityEngine.Random.Range(minMarketPercentValue, 1));
            }
            i++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        marketPopUpBoard.SetActive(true);
        uiCropHigh.sprite = SwitchImage(highCrop);
        uiCropLow.sprite = SwitchImage(lowCrop);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        marketPopUpBoard.SetActive(false);
    }

    public static int GetCropPrice(String crop_type)
    {
        return cropMarketPrices[crop_type];
    }

    public Sprite SwitchImage(String crop_type)
    {
        // Wheat, Potato, Tomato, Strawberry, Blackberry, Pepper, Eggplant
        switch (crop_type)
        {
            case "wheat":
                return UICropImage[0];
                break;
            case "potato":
                return UICropImage[1];
                break;
            case "tomato":
                return UICropImage[2];
                break;
            case "strawberry":
                return UICropImage[3];
                break;
            case "blackberry":
                return UICropImage[4];
                break;
            case "pepper":
                return UICropImage[5];
                break;
            case "eggplant":
                return UICropImage[6];
                break;
            default:
                return UICropImage[0];
                break;
        }
    }
}
