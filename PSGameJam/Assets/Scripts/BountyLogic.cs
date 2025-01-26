using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class BountyLogic : MonoBehaviour
{
    // The amount of crop you've harvested for the bounty
    [SerializeField]
    private int bountyCropCount;

    // The bounty number to reach
    [SerializeField]
    private int bountyMaxCount;

    // Max time limit before bounty expires.
    [SerializeField]
    private float bountyStartTime;

    [SerializeField]
    private short bountyMaxTime = 60;


    // Have we started the bounty
    [SerializeField]
    private bool bountyStart;

    // The Crop used for bounty
    [SerializeField]
    private string bountyCrop;

    // Number of bounties completed
    [SerializeField]
    private int bountyNumCompleted;

    
    private PlayerController playerLogic;

    private GameObject playerObj;

    private Dictionary<String, int> cropMarketPrices = new Dictionary<string, int>();

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

    private bool startTimer = false;

    /*
    function that will payout if bounty is complete
    function to generate bounty
    function to pick up new bounty
    function to see if bounty expires. 
    */

    private void Awake() {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerLogic = playerObj.GetComponent<PlayerController>();

        // Set values of Crop for max payout.
        cropMarketPrices.Add("wheat", wheatValue);
        cropMarketPrices.Add("tomato", tomatoValue);
        cropMarketPrices.Add("potato", potatoValue);
        cropMarketPrices.Add("strawberry", strawberryValue);
        cropMarketPrices.Add("pepper", pepperValue);
        cropMarketPrices.Add("eggplant", eggplantValue);
        cropMarketPrices.Add("blackberry", blackberryValue);

    }

    private void Update(){
        if (bountyStart) {
            BountyIsComplete();
            BountyCheckTimer();
        }
        if (startTimer) {
            bountyStartTime = Time.time;
            startTimer = false;
        }
    }



    // Generate new bounty
    private void BountyGenerate() {
        Debug.Log("Generating New Bounty!");
        bountyStart = true;
        startTimer = false;
        // number of crops
        int ncrops = cropMarketPrices.Count;
        Debug.Log("Length of bounty dictionary " + ncrops);
        var ran = UnityEngine.Random.Range(0, ncrops);
        int counter = 0;
        foreach (var key in cropMarketPrices.Keys){
            Debug.Log("Generating bounty Listing keys in dictionary" + key) ;
            if (counter == ncrops){
                bountyCrop = key;
            }

        }
        bountyCropCount = 0;
    }

    private void BountyIsComplete() {
        if (bountyCropCount >= bountyMaxCount) {
            bountyStart = false;
            bountyCropCount = 0;
            bountyNumCompleted += 1;
            // Payout
            playerLogic.wallet += cropMarketPrices[bountyCrop];
            Debug.Log("Congradulations You've completed a bounty! Here is the number of completed bountys: " + bountyNumCompleted);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!bountyStart && Input.GetKeyDown(KeyCode.L)) {
            BountyGenerate();
            startTimer = true;
            Debug.Log("Accepting Bounty");
        }
    }

    public void BountyIncrease(int crop) {
        bountyCropCount += crop;
        Debug.Log("Number of crops havested for bouny: " + bountyCrop);
    }

    // Put timer on bounty
    private void BountyCheckTimer() {
        var currentTime = Time.time;
        var timePassed = bountyStartTime - currentTime;
        Debug.Log("Time passed: " + timePassed);
        if ( timePassed > bountyMaxTime ){
            Debug.Log("Failed to complete bounty!");
            bountyStart = false;
            bountyCropCount = 0;
        }

    }

}
