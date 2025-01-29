
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class EnviromnentManager : MonoBehaviour
{
    [SerializeField]
    private GameObject globalLightPrefab;
    [SerializeField]
    private float lowestLightIntensity;
    [SerializeField]
    private float highestLightIntensity;
    [SerializeField] 
    private float daySwitchIntensity;
    [SerializeField]
    private float dayNightCycleTime;

    [SerializeField]
    private Color nightColor;

    [SerializeField]
    private List<GameObject> nightLightSources;

    private Light2D globalLightGO;
    private float lightIntensityRate;
    private bool dayTime;
    private Color dayColor = Color.white;
    private float colorPoint;

    private Color fromColor;
    private Color toColor;

    private UnityEvent newDayEventCall = new UnityEvent();

    void Awake()
    {
        fromColor = dayColor;
        toColor = nightColor;
        dayTime = true;
        globalLightGO = Instantiate(globalLightPrefab).GetComponent<Light2D>();

        globalLightGO.intensity = highestLightIntensity;

        lightIntensityRate = -((highestLightIntensity - lowestLightIntensity) / dayNightCycleTime) * 2;

        // Set all the night lights off
        foreach (GameObject go in nightLightSources)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        if (globalLightGO.intensity > highestLightIntensity)
        {
            fromColor = dayColor;
            toColor = nightColor;
            colorPoint = 0;
            globalLightGO.intensity = highestLightIntensity;
            lightIntensityRate = -lightIntensityRate;
        }
        else if(globalLightGO.intensity < lowestLightIntensity)
        {
            toColor = dayColor;
            fromColor = nightColor;
            colorPoint = 0;
            globalLightGO.intensity = lowestLightIntensity;
            lightIntensityRate = -lightIntensityRate;
        }

        if (!dayTime && globalLightGO.intensity >= daySwitchIntensity) // IT IS DAY TIME
        {
            // Set all the night lights off
            foreach (GameObject go in nightLightSources)
            {
                go.SetActive(false);
            }
            newDayEventCall.Invoke();
            dayTime = true;
        }
        else  if (dayTime && globalLightGO.intensity <= daySwitchIntensity) // IT IS NIGHT TIME
        {
            // Set all the night lights on
            foreach (GameObject go in nightLightSources)
            {
                go.SetActive(true);
            }
            dayTime = false;
        };

        colorPoint += Time.deltaTime / (dayNightCycleTime / 2);
        globalLightGO.color = Color.Lerp(fromColor, toColor, colorPoint);
        globalLightGO.intensity += lightIntensityRate * Time.deltaTime;
    }

    public void AddEventCall(UnityAction func)
    {
        newDayEventCall.AddListener(func);
    }

    public float GetDayTimeLength()
    {
        return dayNightCycleTime;
    }
}
