using System;
using UnityEngine;

public class SeedHolder : MonoBehaviour
{
    [SerializeField]
    private TextMesh seedBinText;

    [SerializeField]
    public string seedHolderName;

    [SerializeField]
    private string showText = "{Press F to Refuel Seeds}";

    [SerializeField]
    private bool seedRefuelAllowed;

    private PlayerController playerLogic;

    private GameObject playerObj;

    [SerializeField]
    private Int16 maxSeedRefuel = 25;

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerLogic = playerObj.GetComponent<PlayerController>();
        seedBinText.text = showText;
        seedBinText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (seedRefuelAllowed && Input.GetKeyDown(KeyCode.F))
        {
            Refuel();
        }
    }

    private void Refuel()
    {
        playerLogic.seeds.SeedLevel(maxSeedRefuel, playerLogic.seeds.currentSeed);
        Debug.Log(seedHolderName+":"+ playerLogic.seeds.GetSeedCount(seedHolderName));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            seedBinText.gameObject.SetActive(true);
            seedRefuelAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            seedBinText.gameObject.SetActive(false);
            seedRefuelAllowed = false;
        }
    }
}
