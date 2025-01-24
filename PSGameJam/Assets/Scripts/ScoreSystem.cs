using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    public TextMeshProUGUI moneyText;
    float money = 0.0f;

    private void Awake() {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText.text = "CURRENCY: $" + money;

    }
    void Update()
    {
        moneyText.text = "CURRENCY: $" + money;

    }

    // Update is called once per frame
    public void AddPoint() {
        money += 1;
    }
}
