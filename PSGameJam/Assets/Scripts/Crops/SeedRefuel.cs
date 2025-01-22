using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
    
public class SeedRefuel : MonoBehaviour
{

    [SerializeField]
     private TextMesh refuelText;
     private bool refuelAllowed;

    private PlayerController player;

    private SeedLogic seeds;

     private void Start () {
        refuelText.gameObject.SetActive(false);
     }

     private void Update () {
        if (refuelAllowed && Input.GetKeyDown(KeyCode.E)) {
            var seedType = "wheat";
            Refuel(seedType);
        }
     }

     private void Refuel (String seedType) {
        seeds.SeedLevel(25,seedType);
     }

     private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player")) {
            refuelText.gameObject.SetActive(true);
            refuelAllowed = true;
        }
     }

      private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name.Equals("Player")) {
            refuelText.gameObject.SetActive(false);
            refuelAllowed = false;
        }
     }

}
