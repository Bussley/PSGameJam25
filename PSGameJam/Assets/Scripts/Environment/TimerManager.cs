using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static async void AddTimer(Action func, float duration) {
        var end = Time.time + duration;

        while(Time.time < end)
        {
            await Task.Yield();
        }

        func();
    }

}
