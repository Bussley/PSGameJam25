using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static List<Tuple<Action, float>> timerList = new List<Tuple<Action, float>>();

    private void Update() {
        // Loop through and call any actions that met timer
        for (int i = 0; i < timerList.Count; i++)
        {
            if (timerList[i].Item2 <= Time.time)
            {
                timerList[i].Item1();
            }
        }
        timerList.RemoveAll(item => item.Item2 <= Time.time);
    }

    public static void AddTimer(Action func, float duration) {
        timerList.Add(new Tuple<Action, float>(func, Time.time + duration));
    }
}
