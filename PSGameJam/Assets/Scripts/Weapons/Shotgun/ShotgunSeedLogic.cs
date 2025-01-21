using UnityEditor;
using UnityEngine;
using System;

public class ShotgunSeedLogic : MonoBehaviour
{
    public void RemoveShotgunBullet(float time){

        Action shotgunToLandFunc = () =>
        {
            Destroy(gameObject);
        };
        TimerManager.AddTimer(shotgunToLandFunc, time);
    }
}
