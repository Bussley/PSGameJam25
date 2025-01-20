using UnityEditor;
using UnityEngine;
using System;

public class ShotgunSeedLogic : MonoBehaviour
{
    public void RemoveShotgunBullet(float time){

        Action shotgunToLandFunc = () =>
        {
            TileManager.ChangeTile(transform.position);
            Destroy(gameObject);
        };
        TimerManager.AddTimer(shotgunToLandFunc, time);
    }
}
