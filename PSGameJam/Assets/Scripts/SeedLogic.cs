using System;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeedLogic : MonoBehaviour
{
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
    public string currentSeed = "wheat";

    [SerializeField]    
    private String[] typesOfSeeds = {
        "wheat", // 0
        "tomato", // 1
        "pepper", // 2
        "strawberry", // 3
        "potato", // 4
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start() {
        tomatoCount = 0;
        wheatCount = 25;
        potatoCount = 0;
        pepperCount = 0;
        strawBerryCount = 0;
    }

    public void SeedLevel(int num,String seedType) {
        switch (seedType) {
            case "wheat":
                wheatCount += num;
                break;
            case "tomato":
                tomatoCount += num;
                break;
            case "pepper":
                pepperCount += num;
                break;
            case "strawberry":
                strawBerryCount += num;
                break;
            case "potato":
                potatoCount += num;
                break;                                                
        }
    }

    public int GetSeedCount(String seedType) {
        switch (seedType) {
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

    public void NextSeed (InputAction.CallbackContext context) {
        if (context.control.name == "u" && context.canceled) {
            int num = System.Array.IndexOf(typesOfSeeds, currentSeed);
            if (num >= typesOfSeeds.Length -1) {
                num = 0;
            }
            else {
                num += 1;
            }
            currentSeed = typesOfSeeds[num];
            UnityEngine.Debug.Log(currentSeed);
        }
     }

    public int ShootSeed(int seedShot) {
        String seedType = currentSeed;
        switch (seedType) {
            case "wheat":
                int wnum = wheatCount - seedShot;
                if (wnum <= 0) {
                    wheatCount = 0;
                }
                else
                {
                    wheatCount -= seedShot;
                }
                return wheatCount;
            case "tomato":
                int tnum = wheatCount - seedShot;
                if (tnum <= 0) {
                    tomatoCount = 0;
                }
                else
                {
                    tomatoCount -= seedShot;
                }            
                return tomatoCount;
            case "pepper":
                int pnum = wheatCount - seedShot;
                if (pnum <= 0) {
                    pepperCount = 0;
                }
                else
                {
                    pepperCount -= seedShot;
                }            
                return pepperCount;
            case "strawberry":
                int snum = wheatCount - seedShot;
                if (snum <= 0) {
                    strawBerryCount = 0;
                }
                else
                {
                    strawBerryCount -= seedShot;
                } 
                return strawBerryCount;
            case "potato":
                int ponum = wheatCount - seedShot;
                if (ponum <= 0) {
                    potatoCount = 0;
                }
                else
                {
                    potatoCount -= seedShot;
                }
                return potatoCount;
            default:
                return 0;
        }
    }
}
