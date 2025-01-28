using System;
using UnityEngine;

public class SwitchSnowySprite : MonoBehaviour
{
    [SerializeField] 
    private Sprite regularSprite;
    [SerializeField]
    private Sprite snowSprite;

    private float daylength;

    void Awake()
    {
    }

    private void Start()
    {
        WeatherManager.AddSnowListener(RandomlySwitch);
        daylength = GameObject.FindGameObjectWithTag("UtilityManager").GetComponent<EnviromnentManager>().GetDayTimeLength();
    }

    void RandomlySwitch()
    {
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

        if(WeatherManager.Snowing())
            GetComponent<SpriteRenderer>().sprite = snowSprite;
        else
            GetComponent<SpriteRenderer>().sprite = regularSprite;
    }
}
