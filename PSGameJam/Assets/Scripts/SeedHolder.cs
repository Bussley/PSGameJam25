using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class SeedHolder : MonoBehaviour
{

    [SerializeField]
    private string showText = "{Press F to Refuel Seeds}";

    [SerializeField]
    private bool seedRefuelAllowed;

    private PlayerController playerLogic;
    private int basdf;
    private GameObject playerObj;

    [SerializeField]
    private Int16 maxSeedRefuel = 25;
    
    private Dictionary<String, int> SeedPrices = new Dictionary<string, int>();

        // Values of each Crop for bounty
    [SerializeField]
    private int wheatValue = 1;
    [SerializeField]
    private int potatoValue = 2;
    [SerializeField]
    private int tomatoValue = 5;
    [SerializeField]
    private int strawberryValue = 7;
    [SerializeField]
    private int pepperValue = 3;
    [SerializeField]
    private int eggplantValue = 4;
    [SerializeField]
    private int blackberryValue = 6;

    private GameObject seedCrateObj;


    private void Awake()
    {
        seedCrateObj = GameObject.FindGameObjectWithTag("SeedCrateText");
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerLogic = playerObj.GetComponent<PlayerController>();
        seedCrateObj.SetActive(false);
        seedRefuelAllowed = false;
        

        // Set values of Crop for max payout.
        SeedPrices.Add("wheat", wheatValue);
        SeedPrices.Add("tomato", tomatoValue);
        SeedPrices.Add("potato", potatoValue);
        SeedPrices.Add("strawberry", strawberryValue);
        SeedPrices.Add("pepper", pepperValue);
        SeedPrices.Add("eggplant", eggplantValue);
        SeedPrices.Add("blackberry", blackberryValue);
        showText = "Refel " + playerLogic.seeds.currentSeed;
        seedCrateObj.GetComponent<TMP_Text>().text = showText;

    }

    private void Update()
    {
        showText = "Refel " + playerLogic.seeds.currentSeed;
        seedCrateObj.GetComponent<TMP_Text>().text = showText;
        if (seedRefuelAllowed && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Filling up Seed");
            Refuel();
        }
    }

    private void Refuel()
    {
        BuySeeds();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            seedCrateObj.gameObject.SetActive(true);
            seedRefuelAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Debug.Log("Good by. Please come back to get more seeds!");
            seedCrateObj.gameObject.SetActive(false);
            seedRefuelAllowed = false;
        }
    }

    private void BuySeeds(){
        var currentMoney = playerLogic.wallet;
        var currentSeed = playerLogic.seeds.currentSeed;
        var seedPrice = SeedPrices[currentSeed];

        if (seedPrice > currentMoney) {
            Debug.Log("You don't have any money. You can't buy any seeds!");
        }
        else if (seedPrice <= currentMoney) {
            // Subtract wallet money to buys seeds
            Debug.Log("Buying seed:" + currentSeed + " For price: "+ seedPrice);
            playerLogic.wallet -= seedPrice;
            playerLogic.seeds.SeedLevel(maxSeedRefuel, playerLogic.seeds.currentSeed);
            Debug.Log(playerLogic.seeds.currentSeed+":"+ playerLogic.seeds.GetSeedCount(playerLogic.seeds.currentSeed));

        }
        else {
            Debug.Log("IDK something weird going you need to look into this");
        }
    }
}
