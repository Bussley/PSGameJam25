using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeatherManager : MonoBehaviour { 
    enum Weather {
        Sunny,
        Rain,
        Snowy
    };

    [SerializeField]
    private GameObject rainPrefab;

    [SerializeField]
    private GameObject snowPrefab;
    [SerializeField]
    private GameObject snowFall;
    [SerializeField]
    private float snowFallRate;
    [SerializeField]
    private float snowMeltRate;

    private Weather m_Weather;
    private GameObject rainParticaleObject;
    private GameObject snowParticaleObject;
    private float nextSnowSpawn;
    private float nextSnowMelt;

    private static bool raining;
    private static bool snowing;
    private static UnityEvent rainingWeather = new UnityEvent();
    private static UnityEvent snowWeather = new UnityEvent();
    private Stack<GameObject> snowObjects = new Stack<GameObject>();

    private static int amountSnowPowder = 0;

    void Awake()
    {
        rainParticaleObject = Instantiate(rainPrefab, GameObject.FindGameObjectWithTag("MainCamera").transform);
        snowParticaleObject = Instantiate(snowPrefab, GameObject.FindGameObjectWithTag("MainCamera").transform);
        rainParticaleObject.GetComponent<ParticleSystem>().Stop();
        snowParticaleObject.GetComponent<ParticleSystem>().Stop();
        m_Weather = Weather.Sunny;
        raining = false;
        snowing = false;
        nextSnowSpawn = 0;
        nextSnowMelt = 0;
    }

    void Update()
    {
        if (m_Weather == Weather.Snowy)
            SnowSpawnLogic();
        else
            SnowMeltLogic();
    }

    private void SnowSpawnLogic()
    {
        // Make snow fall
        if (amountSnowPowder <= 400 && nextSnowSpawn < Time.time)
        {
            Vector3 snowspawn = TileManager.GetSpawnableRandomPosition();

            if (snowspawn == Vector3.one) 
                return;

            GameObject snow = Instantiate(snowFall, transform);
            snowObjects.Push(snow);
            snow.transform.position = snowspawn + new Vector3(0.0f, 0.25f, 0.0f);
            nextSnowSpawn = Time.time + snowFallRate;
            amountSnowPowder += 1;
        }
    }
    private void SnowMeltLogic()
    {
        if(snowObjects.Count == 0) return;

        // Make snow fall
        if (nextSnowMelt < Time.time)
        {
            // Might have already delete snow objects
            while (snowObjects.Peek() == null)
            {
                if (snowObjects.Count == 0) return;
                snowObjects.Pop();
            }

            Destroy(snowObjects.Pop());
            nextSnowMelt = Time.time + snowMeltRate;
            amountSnowPowder -= 1;
        }
    }

    public void RollWather()
    {
        int ran = UnityEngine.Random.Range(0, 3);

        switch (ran)
        {
            case 0:
                rainParticaleObject.GetComponent<ParticleSystem>().Stop();
                snowParticaleObject.GetComponent<ParticleSystem>().Stop();
				rainParticaleObject.GetComponent<AudioSource>().Stop();
				snowParticaleObject.GetComponent<AudioSource>().Stop();
                snowWeather.Invoke();
                raining = false;
                snowing = false;
                m_Weather = Weather.Sunny; 
                break;
            case 1:
                raining = true;
                snowing = false;
                rainingWeather.Invoke();
                snowWeather.Invoke();
                rainParticaleObject.GetComponent<ParticleSystem>().Play();
                snowParticaleObject.GetComponent<ParticleSystem>().Stop();
				rainParticaleObject.GetComponent<AudioSource>().Play();
				snowParticaleObject.GetComponent<AudioSource>().Stop();
                m_Weather = Weather.Rain;
                break;
            case 2:
                raining = false;
                snowing = true;
                snowWeather.Invoke();
                rainParticaleObject.GetComponent<ParticleSystem>().Stop();
                snowParticaleObject.GetComponent<ParticleSystem>().Play();
				rainParticaleObject.GetComponent<AudioSource>().Stop();
				snowParticaleObject.GetComponent<AudioSource>().Play();
                m_Weather = Weather.Snowy;
                break;
            default:
                break;
        }
    }

    public static void AddRainListener(UnityAction func)
    {
        rainingWeather.AddListener(func);
    }

    public static void AddSnowListener(UnityAction func)
    {
        snowWeather.AddListener(func);
    }

    public static bool Raining()
    {
        return raining;
    }

    public static bool Snowing()
    {
        return snowing;
    }

    public static void DestroySnowPowder()
    {
        amountSnowPowder -= 1;
    }
}
