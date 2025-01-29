using System;
using System.Collections.Generic;
using UnityEngine;

public class SnowPowderLogic : MonoBehaviour
{
    private void OnDestroy()
    {
        WeatherManager.DestroySnowPowder();
    }
}
