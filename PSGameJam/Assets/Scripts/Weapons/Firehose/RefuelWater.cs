using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class RefuelWater : MonoBehaviour
{
    [SerializeField]
    private TextMesh refuelText;
    private bool refuelAllowed;

    [SerializeField]
    private PlayerController player;
    private GameObject UIMBCHB;
    private UnityEngine.UI.Slider UIMBCSlider;


    private void Start()
    {
        // UI Overheat bar logic
        UIMBCHB = GameObject.FindGameObjectWithTag("UIMBCHydroBar");
        UIMBCSlider = UIMBCHB.GetComponent<UnityEngine.UI.Slider>();
        
        refuelText.gameObject.SetActive(false);
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
        if (collision.gameObject.name.Equals("Player"))
        {
            refuelText.gameObject.SetActive(true);
            refuelAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            refuelText.gameObject.SetActive(false);
            refuelAllowed = false;
        }
    }

}
