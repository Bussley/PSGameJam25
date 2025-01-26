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

    private PlayerController player;

    [SerializeField]
    private Int16 maxSeedRefuel = 25;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        player.seeds.SeedLevel(maxSeedRefuel, seedHolderName);
        Debug.Log(seedHolderName+":"+ player.seeds.GetSeedCount(seedHolderName));
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
