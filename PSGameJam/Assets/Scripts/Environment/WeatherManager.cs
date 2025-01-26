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

    private Weather m_Weather;

    void Awake()
    {
        m_Weather = Weather.Sunny;
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

    }

    private void SnowLogic()
    {
        Vector3 snowspawn = TileManager.GetSpawnableRandomPosition();
    }

    public void RollWather()
    {
        int ran = UnityEngine.Random.Range(0, 2);

        switch (ran)
        {
            case 0:
                rainPrefab.SetActive(false);
                snowPrefab.SetActive(false);
                m_Weather = Weather.Sunny; 
                break;
            case 1:
                rainPrefab.SetActive(true);
                snowPrefab.SetActive(false);
                m_Weather = Weather.Rain;
                break;
            case 2:
                rainPrefab.SetActive(false);
                snowPrefab.SetActive(true);
                m_Weather = Weather.Snowy;
                break;
            default:
                break;
        }
    }
}
