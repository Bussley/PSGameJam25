using UnityEngine;

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

    void Awake()
    {
        rainParticaleObject = Instantiate(rainPrefab, GameObject.FindGameObjectWithTag("MainCamera").transform);
        snowParticaleObject = Instantiate(snowPrefab, GameObject.FindGameObjectWithTag("MainCamera").transform);
        m_Weather = Weather.Sunny;
        nextSnowSpawn = Time.time;
    }

    void Update()
    {
        switch (m_Weather)
        {
            case Weather.Rain:
                RainLogic();
                break;
            case Weather.Snowy:
                SnowLogic();
                break;
            default:
                break;
        }
    }

    private void RainLogic()
    {
        // Rain hydrate plants
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
                rainParticaleObject.SetActive(false);
                snowParticaleObject.SetActive(false);
                m_Weather = Weather.Snowy; 
                break;
            case 1:
                Debug.Log("Rain");
                rainParticaleObject.SetActive(true);
                snowParticaleObject.SetActive(false);
                m_Weather = Weather.Snowy;
                break;
            case 2:
                Debug.Log("SNOW");
                rainParticaleObject.SetActive(false);
                snowParticaleObject.SetActive(true);
                m_Weather = Weather.Snowy;
                break;
            default:
                break;
        }
    }
}
