using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class RefuelWater : MonoBehaviour
{
    [SerializeField]
    private string refuelText;
    private bool refuelAllowed;

    [SerializeField]
    private PlayerController player;
    private GameObject UIMBCHB;
    private GameObject wText;
    private UnityEngine.UI.Slider UIMBCSlider;


    private void Start()
    {
        // UI Overheat bar logic
        UIMBCHB = GameObject.FindGameObjectWithTag("UIMBCHydroBar");
        UIMBCSlider = UIMBCHB.GetComponent<UnityEngine.UI.Slider>();

        wText = GameObject.FindGameObjectWithTag("WaterRefuelText");
        wText.GetComponent<TMP_Text>().text = "";
        //wText.SetActive(false);
    }

    private void Update()
    {
        if (refuelAllowed && Input.GetKeyDown(KeyCode.F))
        {
            Refuel();
        }
    }

    private void Refuel()
    {
        player.PlayerWaterTankLevel(100.0f);
        UIMBCSlider.value = 100.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //wText.gameObject.SetActive(true);
            wText.GetComponent<TMP_Text>().text = refuelText;
            refuelAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //wText.SetActive(false);
            wText.GetComponent<TMP_Text>().text = "";
            refuelAllowed = false;
        }
    }

}
