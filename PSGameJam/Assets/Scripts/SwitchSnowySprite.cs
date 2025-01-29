using System;
using UnityEngine;

public class SwitchSnowySprite : MonoBehaviour
{
    [SerializeField] 
    private Sprite regularSprite;
    [SerializeField]
    private Sprite snowSprite;

    private float daylength;
    private bool alreadysnowy;

    void Awake()
    {
        alreadysnowy = false;
    }

    private void Start()
    {
        WeatherManager.AddSnowListener(RandomlySwitch);
        daylength = GameObject.FindGameObjectWithTag("UtilityManager").GetComponent<EnviromnentManager>().GetDayTimeLength();
    }

    void RandomlySwitch()
    {
        if (WeatherManager.Snowing() && alreadysnowy) return;

        Action action = () =>
        {
            SwitchSnow();
        };
        var ran = UnityEngine.Random.Range(0, daylength / 2);
        TimerManager.AddTimer(action, ran);
    }

    void SwitchSnow()
    {
        if (this == null) return;

        if (WeatherManager.Snowing())
        {
            GetComponent<SpriteRenderer>().sprite = snowSprite;
            alreadysnowy = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = regularSprite;
            alreadysnowy = false;
        }
    }
}
