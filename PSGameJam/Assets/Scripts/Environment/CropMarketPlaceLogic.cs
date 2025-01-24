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

    [SerializeField]
    private float rollMarketTimer;

    private GameObject marketPopUpBoard;
    private Dictionary<String, int> baseCropMarketPrices = new Dictionary<string, int>();
    private Dictionary<String, int> cropMarketPrices = new Dictionary<string, int>();

    void Awake()
    {
        marketPopUpBoard = transform.GetChild(0).gameObject;
        marketPopUpBoard.SetActive(false);

        CropScriptableObject[] allcrops = Resources.LoadAll<CropScriptableObject>("ScriptableObjects/Crops/");

        foreach(CropScriptableObject obj in allcrops)
        {
            baseCropMarketPrices.Add(obj.cropType, obj.baseMarketValue);
        }

        TimerManager.AddTimer(RollMarket, rollMarketTimer);
    }

    public void RollMarket()
    {
        cropMarketPrices = baseCropMarketPrices;
        foreach(var c in cropMarketPrices.Keys)
        {
            cropMarketPrices[c] = (int)(baseCropMarketPrices[c] * UnityEngine.Random.Range(minMarketPercentValue, maxMarketPercentValue));
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
}
