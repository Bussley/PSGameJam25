using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.Burst.Intrinsics;
using System.Linq;


public class BountyLogic : MonoBehaviour
{
    // The amount of crop you've harvested for the bounty
    [SerializeField]
    public int bountyRewardValue;
    // The amount of crop you've harvested for the bounty
    [SerializeField]
    public int bountyCropCount;

    // The bounty number to reach
    [SerializeField]
    private int bountyMaxCount;

    // Max time limit before bounty expires.
    [SerializeField]
    private float bountyStartTime;

    [SerializeField]
    private float bountyMaxTime = 200.0f;

    private bool firstBounty = true;

    private bool hasWon = false;


    // Have we started the bounty
    [SerializeField]
    public bool bountyStart;

    // The Crop used for bounty
    [SerializeField]
    public string bountyCrop;

    // Number of bounties completed
    [SerializeField]
    private int bountyNumCompleted;


    private PlayerController playerLogic;

    private GameObject playerObj;

    /*
        UI stuff
        naming shortnames
        UI = UI
        OC = Order Container
        OCOCA = Order Container Order Amount
        OCOCA = Order Container Crop Icon
        UITT = UI Task Text
        UIM = UI Money
    */
    private GameObject UIOCOA;
    private GameObject UITT;
    private GameObject UIOC;


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

    private bool interactWithBoard;

    // UI Order Container Time Bar
    private GameObject UIOCTB;
    private UnityEngine.UI.Slider UIOCTBSlider;

    //sound
    private AudioSource sfx;

    [SerializeField]
    private AudioClip pickQuest;
    [SerializeField]
    private AudioClip winQuest;
    [SerializeField]
    private AudioClip loseQuest;
    [SerializeField]
    private AudioClip ticktock;
    private bool ticktime = true;

    [SerializeField]
    private UnityEvent winEvent;

    private Dictionary<String, bool> questLine = new Dictionary<string, bool>();


    /*
    function that will payout if bounty is complete
    function to generate bounty
    function to pick up new bounty
    function to see if bounty expires. 
    */

    private void Start() {
        bountyStart = false;

    }

    private void Awake() {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerLogic = playerObj.GetComponent<PlayerController>();
        sfx = transform.GetChild(0).GetComponent<AudioSource>();

        bountyRewardValue = 100;
        // Set values of Crop for max payout.
        cropMarketPrices.Add("wheat", wheatValue);
        cropMarketPrices.Add("tomato", tomatoValue);
        cropMarketPrices.Add("potato", potatoValue);
        cropMarketPrices.Add("strawberry", strawberryValue);
        cropMarketPrices.Add("pepper", pepperValue);
        cropMarketPrices.Add("eggplant", eggplantValue);
        cropMarketPrices.Add("blackberry", blackberryValue);

        questLine.Add("wheat", false);
        questLine.Add("potato", false);
        questLine.Add("pepper", false);
        questLine.Add("eggplant", false);
        questLine.Add("tomato", false);
        questLine.Add("blackberry", false);
        questLine.Add("strawberry", false);

        interactWithBoard = false;
        bountyStart = false;
        bountyMaxCount = 10;
        UIOCOA = GameObject.FindGameObjectWithTag("UIOrderContainerOrderAmount");
        UITT = GameObject.FindGameObjectWithTag("UITaskText");
        UIOC = GameObject.FindGameObjectWithTag("UIOrderContainer");
        UIOCTB = GameObject.FindGameObjectWithTag("UIOCTimeBar");
        UIOCTBSlider = UIOCTB.GetComponent<UnityEngine.UI.Slider>();
        UIOC.SetActive(false);


        //BountyGenerate();

    }

    private void Update() {
        if (bountyStart) {
            //UIOCTBSlider.value = bountyMaxTime;
            UIOC.SetActive(true);
            BountyIsComplete();
            BountyCheckTimer();
            UIOCOA.GetComponent<TMP_Text>().text = bountyCropCount + "/" + bountyMaxCount;
            UITT.GetComponent<TMP_Text>().text = "Please harvest " + bountyMaxCount + " " + bountyCrop + "o. Reward: $" + bountyRewardValue;
        }
        else {
            if (UIOC.active) {
                UIOC.SetActive(false);
            }

            UIOCOA.GetComponent<TMP_Text>().text = "";
            UITT.GetComponent<TMP_Text>().text = "Go to the Stalk Market to accept an order.";
            bountyCrop = "";
        }
        if (startTimer) {
            bountyStartTime = Time.time;
            startTimer = false;
        }
        if (interactWithBoard) {
            if (!bountyStart && Input.GetKeyDown(KeyCode.F)) {
                QuestLineGenerate();
                //BountyGenerate();
                startTimer = true;
                Debug.Log("Accepting Bounty");
                //playing sound
                sfx.PlayOneShot(pickQuest);
                ticktime = true;
            }
        }
    }

    // Generate new bounty
    private void BountyGenerate() {
        Debug.Log("Generating New Bounty!");
        bountyStart = true;
        startTimer = false;
        /*
        if (firstBounty) {
            int ncrops = cropMarketPrices.Count;
            Debug.Log("First bounty will be Wheat!");
            bountyCrop = "wheat";
            firstBounty = false;
            bountyMaxTime = 600.0f;
        }
        else
        {
        */
        bountyMaxTime = 300.0f;
        // number of crops
        int ncrops = cropMarketPrices.Count;
        Debug.Log("Length of bounty dictionary " + ncrops);
        var ran = UnityEngine.Random.Range(0, ncrops);
        int counter = 0;
        foreach (var key in cropMarketPrices.Keys) {

            if (counter == ran) {
                Debug.Log("Generating bounty: " + key);
                bountyCrop = key;
            }

            counter++;

        }
        switch (bountyCrop) {
            case "wheat":
                bountyMaxCount = 15;
                bountyRewardValue = 200;
                break;
            case "tomato":
                bountyMaxCount = 34;
                bountyRewardValue = 1400;
                break;
            case "pepper":
                bountyMaxCount = 20;
                bountyRewardValue = 700;
                break;
            case "strawberry":
                bountyMaxCount = 20;
                bountyRewardValue = 6000;
                break;
            case "potato":
                bountyMaxCount = 18;
                bountyRewardValue = 400;
                break;
            case "blackberry":
                bountyMaxCount = 12;
                bountyRewardValue = 4000;
                break;
            case "eggplant":
                bountyMaxCount = 14;
                bountyRewardValue = 900;
                break;
        }
        bountyCropCount = 0;
    }

    private void QuestLineGenerate() {
        foreach (string key in questLine.Keys) {
            if (!questLine[key]) {
                bountyStart = true;
                startTimer = true;
                Debug.Log("Generating bounty: " + key);
                bountyCrop = key;
                bountyCropCount = 0;
                switch (key) {
                    case "wheat":
                        bountyMaxTime = 600.0f;
                        bountyMaxCount = 10;
                        bountyRewardValue = 200;
                        break;
                    case "tomato":
                        bountyMaxTime = 300.0f;
                        bountyMaxCount = 34;
                        bountyRewardValue = 1400;
                        break;
                    case "pepper":
                        bountyMaxTime = 300.0f;
                        bountyMaxCount = 20;
                        bountyRewardValue = 700;
                        break;
                    case "strawberry":
                        bountyMaxTime = 300.0f;
                        bountyMaxCount = 15;
                        bountyRewardValue = 6000;
                        break;
                    case "potato":
                        bountyMaxTime = 300.0f;
                        bountyMaxCount = 14;
                        bountyRewardValue = 400;
                        break;
                    case "blackberry":
                        bountyMaxTime = 300.0f;
                        bountyMaxCount = 7;
                        bountyRewardValue = 4000;
                        break;
                    case "eggplant":
                        bountyMaxTime = 300.0f;
                        bountyMaxCount = 14;
                        bountyRewardValue = 900;
                        break;
                }
                UIOCTBSlider.maxValue = bountyMaxTime;
                break;
            } else if (questLine.Values.Last()) {
                BountyGenerate();
                UIOCTBSlider.maxValue = bountyMaxTime;
            }
        }
    }

    private void BountyIsComplete() {
        if (bountyCropCount >= bountyMaxCount) {
            bountyStart = false;
            bountyCropCount = 0;
            bountyNumCompleted += 1;
            Debug.Log("Crop" + bountyCrop);
            // Payout
            //playerLogic.wallet += cropMarketPrices[bountyCrop];
            playerLogic.wallet += bountyRewardValue;
            Debug.Log("Congradulations You've completed a bounty! Here is the number of completed bountys: " + bountyNumCompleted);
            Debug.Log("Player money: " + playerLogic.wallet);
            questLine[bountyCrop] = true;
            //playing sound
            sfx.PlayOneShot(winQuest);
            bountyCrop = "";
            CheckCompletedBounties();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        interactWithBoard = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactWithBoard = false;
    }

    public void BountyIncrease(int crop) {
        bountyCropCount += crop;
        Debug.Log("Number of crops havested for bouny: " + bountyCrop);
    }

    // Put timer on bounty
    private void BountyCheckTimer() {

        var currentTime = Time.time;
        var timePassed = currentTime - bountyStartTime;
        UIOCTBSlider.value = timePassed;
        //PLAYING HURRY UP SOUND ONCE
        if (timePassed >= bountyMaxTime && ticktime)
        {
            //playing sound
            sfx.PlayOneShot(ticktock);
            ticktime = false;
        }
        if (timePassed > bountyMaxTime) {
            Debug.Log("Failed to complete bounty!");
            bountyStart = false;
            bountyCropCount = 0;
            //playing sound
            sfx.PlayOneShot(loseQuest);
        }
        UIOCTBSlider.value = bountyMaxTime - timePassed;
    }

    private void CheckCompletedBounties()
    {
        if (bountyNumCompleted >= 7 && !hasWon)
        {
            hasWon = true;
            winEvent.Invoke();
        }
    }
}
