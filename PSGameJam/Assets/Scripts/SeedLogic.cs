using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SeedLogic : MonoBehaviour
{
    private int tomatoSeedCount;
    private int wheatSeedCount;
    private int potatoSeedCount;
    private int pepperSeedCount;
    private int strawBerrySeedCount;
    private int blackberrySeedCount;
    private int eggplantSeedCount;

    public static string currentSeed = "wheat";

    public String[] typesOfSeeds = {
        "wheat", // 0
        "tomato", // 1
        "pepper", // 2
        "strawberry", // 3
        "potato", // 4
        "blackberry", // 5
        "eggplant", // 6
    };


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
        tomatoSeedCount = 15;
        wheatSeedCount = 15;
        potatoSeedCount = 15;
        pepperSeedCount = 15;
        strawBerrySeedCount = 10;
        blackberrySeedCount = 10;
        eggplantSeedCount = 15;

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
            case "blackberry":
                blackberrySeedCount += num;
                break;
            case "eggplant":
                blackberrySeedCount += num;
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
            case "blackberry":
                return blackberrySeedCount;
            case "eggplant":
                return eggplantSeedCount;
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
		//Testing generous seeds=
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
            case "blackberry":
                int bbnum = blackberrySeedCount - seedShot;
                if (bbnum <= 0) {
                    blackberrySeedCount = 0;
                }
                else
                {
                    blackberrySeedCount -= seedShot;
                } 
                return blackberrySeedCount;
            case "eggplant":
                int egnum = eggplantSeedCount - seedShot;
                if (egnum <= 0) {
                    eggplantSeedCount = 0;
                }
                else
                {
                    eggplantSeedCount -= seedShot;
                    ;
                }
                return eggplantSeedCount;
            default:
                return 0;
        }
    }
}
