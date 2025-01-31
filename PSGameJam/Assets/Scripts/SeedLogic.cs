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
	private SFXController sfx;
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


    // Out of Ammo
    private GameObject OOA;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake() {
        OOA = GameObject.FindGameObjectWithTag("OutOfAmmo");
        tomatoSeedCount = 5;
        wheatSeedCount = 30;
        potatoSeedCount = 20;
        pepperSeedCount = 15;
        strawBerrySeedCount = 3;
        blackberrySeedCount = 3;
        eggplantSeedCount = 10;
        OOA.SetActive(false);
		sfx = GetComponent<SFXController>();
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
                eggplantSeedCount += num;
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
                    OOA.SetActive(true);
                    Action action = () => {OOA.SetActive(false);};
                    TimerManager.AddTimer(action,1.5f);                    
                    wheatSeedCount = 0;
					sfx.playSound(9);
                }
                else
                {
                    wheatSeedCount -= seedShot;
                }
                return wheatSeedCount;
            case "tomato":
                int tnum = tomatoSeedCount - seedShot;
                if (tnum <= 0) {
                    OOA.SetActive(true);
                    Action action = () => {OOA.SetActive(false);};
                    TimerManager.AddTimer(action,1.5f);                    
                    tomatoSeedCount = 0;
					sfx.playSound(9);
                }
                else
                {
                    tomatoSeedCount -= seedShot;
                }            
                return tomatoSeedCount;
            case "pepper":
                int pnum = pepperSeedCount - seedShot;
                if (pnum <= 0) {
                    OOA.SetActive(true);
                    Action action = () => {OOA.SetActive(false);};
                    TimerManager.AddTimer(action,1.5f);                    
                    pepperSeedCount = 0;
					sfx.playSound(9);
                }
                else
                {
                    pepperSeedCount -= seedShot;
                }            
                return pepperSeedCount;
            case "strawberry":
                int snum = strawBerrySeedCount - seedShot;
                if (snum <= 0) {
                    OOA.SetActive(true);
                    Action action = () => {OOA.SetActive(false);};
                    TimerManager.AddTimer(action,1.5f);                    
                    strawBerrySeedCount = 0;
					sfx.playSound(9);
                }
                else
                {
                    strawBerrySeedCount -= seedShot;
                } 
                return strawBerrySeedCount;
            case "potato":
                int ponum = potatoSeedCount - seedShot;
                if (ponum <= 0) {
                    OOA.SetActive(true);
                    Action action = () => {OOA.SetActive(false);};
                    TimerManager.AddTimer(action,1.5f);
                    potatoSeedCount = 0;
					sfx.playSound(9);
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
					sfx.playSound(9);
                    OOA.SetActive(true);
                    Action action = () => {OOA.SetActive(false);};
                    TimerManager.AddTimer(action,1.5f);
                }
                else
                {
                    blackberrySeedCount -= seedShot;
                } 
                return blackberrySeedCount;
            case "eggplant":
                int egnum = eggplantSeedCount - seedShot;
                if (egnum <= 0) {
                    OOA.SetActive(true);
                    Action action = () => {OOA.SetActive(false);};
                    TimerManager.AddTimer(action,1.5f);
                    eggplantSeedCount = 0;
					sfx.playSound(9);
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
