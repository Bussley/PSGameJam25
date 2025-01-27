using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CropMarketPlaceLogic : MonoBehaviour
{
    [SerializeField]
    private float minMarketPercentValue;
    [SerializeField]
    private float maxMarketPercentValue;

    private GameObject marketPopUpBoard;
    private Dictionary<String, int> baseCropMarketPrices = new Dictionary<string, int>();
    private static Dictionary<String, int> cropMarketPrices = new Dictionary<string, int>();

    public Dictionary<String, int> GetCropMarketPrices() {
        return cropMarketPrices;
    }

    void Awake()
    {
        marketPopUpBoard = transform.GetChild(0).gameObject;
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
        foreach (var c in baseCropMarketPrices)
        {
            cropMarketPrices[c.Key] = (int)(baseCropMarketPrices[c.Key] * UnityEngine.Random.Range(minMarketPercentValue, maxMarketPercentValue));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        marketPopUpBoard.SetActive(true);

        TextMeshPro tm = marketPopUpBoard.GetComponent<TextMeshPro>();

        tm.ClearMesh();

        tm.text = "HI MOM";

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        marketPopUpBoard.SetActive(false);
    }

    public static int GetCropPrice(String crop_type)
    {
        foreach (var c in cropMarketPrices)
            Debug.Log(c.Key);
        return cropMarketPrices[crop_type];
    }
}
