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

    private Weather m_Weather;
    private GameObject rainParticaleObject;
    private GameObject snowParticaleObject;
    private float nextSnowSpawn;

    private static bool raining;
    private static bool snowing;
    private static UnityEvent rainingWeather = new UnityEvent();

    void Awake()
    {
        rainParticaleObject = Instantiate(rainPrefab, GameObject.FindGameObjectWithTag("MainCamera").transform);
        snowParticaleObject = Instantiate(snowPrefab, GameObject.FindGameObjectWithTag("MainCamera").transform);
        m_Weather = Weather.Sunny;
        raining = false;
        snowing = false;
        nextSnowSpawn = Time.time;
    }

    void Update()
    {
        if (m_Weather == Weather.Snowy)
            SnowLogic();
    }

    private void SnowLogic()
    {
        // Make snow fall
        if (nextSnowSpawn < Time.time)
        {
            Vector3 snowspawn = TileManager.GetSpawnableRandomPosition();
            Instantiate(snowFall, snowspawn, new Quaternion());
            nextSnowSpawn = Time.time + snowFallRate;
        }
    }

    public void RollWather()
    {
        int ran = UnityEngine.Random.Range(0, 2);

        switch (ran)
        {
            case 0:
                Debug.Log("SUNNY");
                raining = false;
                snowing = false;
                rainParticaleObject.SetActive(false);
                snowParticaleObject.SetActive(false);
                m_Weather = Weather.Snowy; 
                break;
            case 1:
                Debug.Log("Rain");
                raining = true;
                snowing = false;
                rainingWeather.Invoke();
                rainParticaleObject.SetActive(true);
                snowParticaleObject.SetActive(false);
                m_Weather = Weather.Snowy;
                break;
            case 2:
                Debug.Log("SNOW");
                raining = false;
                snowing = true;
                rainParticaleObject.SetActive(false);
                snowParticaleObject.SetActive(true);
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

    public static bool Raining()
    {
        return raining;
    }

    public static bool Snowing()
    {
        return snowing;
    }
}
