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
    private string showText = "{Press F to Buy Seeds}";

    [SerializeField]
    private bool seedRefuelAllowed;

    private PlayerController playerLogic;
    private int basdf;
    private GameObject playerObj;

    [SerializeField]
    private Int16 maxSeedRefuel = 10;
    
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
    private GameObject seedCrateObj1;

    private string scStext = "< Q   E >";

    private void Awake()
    {
        seedCrateObj = GameObject.FindGameObjectWithTag("SeedCrateText");
        seedCrateObj1 = GameObject.FindGameObjectWithTag("SeedCreateSwitchText");
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerLogic = playerObj.GetComponent<PlayerController>();
        seedCrateObj.SetActive(false);
        seedCrateObj1.SetActive(false);

        seedRefuelAllowed = false;
        

        // Set values of Crop for max payout.
        SeedPrices.Add("wheat", wheatValue);
        SeedPrices.Add("tomato", tomatoValue);
        SeedPrices.Add("potato", potatoValue);
        SeedPrices.Add("strawberry", strawberryValue);
        SeedPrices.Add("pepper", pepperValue);
        SeedPrices.Add("eggplant", eggplantValue);
        SeedPrices.Add("blackberry", blackberryValue);
        showText = "Buy " + SeedLogic.currentSeed;
        seedCrateObj.GetComponent<TMP_Text>().text = showText;
        seedCrateObj1.GetComponent<TMP_Text>().text = scStext;


    }

    private void Update()
    {
        if (SeedLogic.currentSeed == "wheat") {
            showText = "Buy " + SeedLogic.currentSeed + " for FREE!";
        }
        else {
            showText = "Buy " + SeedLogic.currentSeed + "  $" + SeedPrices[SeedLogic.currentSeed];
        }
        seedCrateObj.GetComponent<TMP_Text>().text = showText;
        seedCrateObj1.GetComponent<TMP_Text>().text = scStext;
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
        if (collision.gameObject.tag == "Player")
        {
            seedCrateObj1.gameObject.SetActive(true);
            seedCrateObj.gameObject.SetActive(true);
            seedRefuelAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Good by. Please come back to get more seeds!");
            seedCrateObj.gameObject.SetActive(false);
            seedCrateObj1.gameObject.SetActive(false);

            seedRefuelAllowed = false;
        }
    }

    private void BuySeeds(){
        var currentMoney = playerLogic.wallet;
        var currentSeed = SeedLogic.currentSeed;
        var seedPrice = SeedPrices[currentSeed];

        if (currentSeed == "wheat" && playerLogic.seeds.GetSeedCount(currentSeed) < 40){
            playerLogic.seeds.SeedLevel(maxSeedRefuel, SeedLogic.currentSeed);
        }
        else {
            if (seedPrice > currentMoney) {
                Debug.Log("You don't have any money. You can't buy any seeds!");
            }
            else if (seedPrice <= currentMoney) {
                // Subtract wallet money to buys seeds
                Debug.Log("Buying seed:" + currentSeed + " For price: "+ seedPrice);
                playerLogic.wallet -= seedPrice;
                playerLogic.seeds.SeedLevel(maxSeedRefuel, SeedLogic.currentSeed);
                Debug.Log(SeedLogic.currentSeed+":"+ playerLogic.seeds.GetSeedCount(SeedLogic.currentSeed));
				//playing sound
				GetComponent<AudioSource>().Play();
            }
            else {
                Debug.Log("IDK something weird going you need to look into this");
            }
        }


    }
}
