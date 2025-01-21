using UnityEditor;
using UnityEngine;
using System;

public class ShotgunSeedLogic : MonoBehaviour
{
    public void RemoveShotgunBullet(float time){

        Action shotgunToLandFunc = () =>
        {
            Debug.Log("Tile position -> " + transform.position);
            TileManager.ChangeTile(transform.position);
            Destroy(gameObject);
        };
        TimerManager.AddTimer(shotgunToLandFunc, time);
    }
}
