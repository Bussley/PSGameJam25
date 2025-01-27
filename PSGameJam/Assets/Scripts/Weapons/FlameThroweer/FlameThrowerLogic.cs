using System;
using UnityEngine;
using UnityEngine.VFX;

public class FlameThrowerLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject flameVFX;

    private GameObject flameVFXGO;

    private void Awake()
    {
        flameVFXGO = Instantiate(flameVFX, transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Soil to destroy crop
        if (collision.gameObject.tag == "Soil" && !WeatherManager.Snowing())
            collision.gameObject.GetComponent<SoilLogic>().CharTile();
    }

    public void StopFlame()
    {
        flameVFXGO.GetComponent<VisualEffect>().Stop();

        Action action = () =>
        {
            Destroy(gameObject);
        };

        TimerManager.AddTimer(action, 0.2f);
    }
}
