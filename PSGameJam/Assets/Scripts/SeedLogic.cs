using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class SeedLogic : MonoBehaviour
{
    [SerializeField]
    private int tomatoSeedCount = 0;
    [SerializeField]
    private int wheatSeedCount = 25;
    [SerializeField]
    private int potatoSeedCount = 0;
    [SerializeField]
    private int pepperSeedCount = 0;
    [SerializeField]
    private int strawBerrySeedCount = 0;


    private PlayerController playerLogic;

    private GameObject playerObj;

    [SerializeField]
    public string currentSeed = "wheat";

    [SerializeField]    
    public String[] typesOfSeeds = {
        "wheat", // 0
        "tomato", // 1
        "pepper", // 2
        "strawberry", // 3
        "potato", // 4
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
        tomatoSeedCount = 0;
        wheatSeedCount = 25;
        potatoSeedCount = 0;
        pepperSeedCount = 0;
        strawBerrySeedCount = 0;
    }

    public void SeedLevel(int num,String seedType) {
        switch (seedType) {
            case "wheat":
                wheatSeedCount += num;
                break;
            case "tomato":
                tomatoSeedCount += num;
                break;
            case "pepper":
                pepperSeedCount += num;
                break;
            case "strawberry":
                strawBerrySeedCount += num;
                break;
            case "potato":
                potatoSeedCount += num;
                break;                                                
        }
    }

    public int GetSeedCount(String seedType) {
        switch (seedType) {
            case "wheat":
                return wheatSeedCount;
            case "tomato":
                return tomatoSeedCount;
            case "pepper":
                return pepperSeedCount;
            case "strawberry":
                return strawBerrySeedCount;
            case "potato":
                return potatoSeedCount;
            default:
                return 0;
        }
    }

    public void NextSeed (InputAction.CallbackContext context) {
        int num = System.Array.IndexOf(typesOfSeeds, currentSeed);
        if (context.control.name == "e" && context.canceled) {
            if (num >= typesOfSeeds.Length -1) {
                num = 0;
            }
            else {
                num += 1;
            }
            currentSeed = typesOfSeeds[num];
        }
        else if (context.control.name == "q" && context.canceled) {
            if (num - 1 < 0) {
                num = typesOfSeeds.Length -1;
            }
            else if (num == 0) {
                num = typesOfSeeds.Length -1;
            }
            else {
                num -= 1;
            }
            currentSeed = typesOfSeeds[num];
        }
        UnityEngine.Debug.Log(currentSeed);

     }

    public int ShootSeed(int seedShot) {
        String seedType = currentSeed;
        switch (seedType) {
            case "wheat":
                int wnum = wheatSeedCount - seedShot;
                if (wnum <= 0) {
                    wheatSeedCount = 0;
                }
                else
                {
                    wheatSeedCount -= seedShot;
                }
                return wheatSeedCount;
            case "tomato":
                int tnum = wheatSeedCount - seedShot;
                if (tnum <= 0) {
                    tomatoSeedCount = 0;
                }
                else
                {
                    tomatoSeedCount -= seedShot;
                }            
                return tomatoSeedCount;
            case "pepper":
                int pnum = wheatSeedCount - seedShot;
                if (pnum <= 0) {
                    pepperSeedCount = 0;
                }
                else
                {
                    pepperSeedCount -= seedShot;
                }            
                return pepperSeedCount;
            case "strawberry":
                int snum = wheatSeedCount - seedShot;
                if (snum <= 0) {
                    strawBerrySeedCount = 0;
                }
                else
                {
                    strawBerrySeedCount -= seedShot;
                } 
                return strawBerrySeedCount;
            case "potato":
                int ponum = wheatSeedCount - seedShot;
                if (ponum <= 0) {
                    potatoSeedCount = 0;
                }
                else
                {
                    potatoSeedCount -= seedShot;
                    ;
                }
                return potatoSeedCount;
            default:
                return 0;
        }
    }
}
