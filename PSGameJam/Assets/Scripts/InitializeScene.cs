using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class InitializeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerPrefab;

    [SerializeField]
    private GameObject TileMapPrefab;

    [SerializeField]
    private GameObject UtilityManagerPrefab;

    [SerializeField]
    private GameObject UIPrefab;

    [SerializeField]
    private GameObject DuckPrefab;

    private GameObject MarketBoard;

    void Awake()
    {
        Instantiate(DuckPrefab);
        Instantiate(UIPrefab);
        Instantiate(PlayerPrefab);
        GameObject map = Instantiate(TileMapPrefab);
        TileManager.SetTileMap(map.transform.GetChild(0).GetComponent<Tilemap>());

        GameObject um = Instantiate(UtilityManagerPrefab);

        MarketBoard = GameObject.FindGameObjectWithTag("CropMarketBoard");

        //Add Listeners to day night cycle
        if(MarketBoard != null )
            um.GetComponent<EnviromnentManager>().AddEventCall(() => MarketBoard.GetComponent<CropMarketPlaceLogic>().RollMarket());
        um.GetComponent<EnviromnentManager>().AddEventCall(() => um.GetComponent<EnemyManager>().SpawnScarecrow());
        um.GetComponent<EnviromnentManager>().AddEventCall(() => um.GetComponent<WeatherManager>().RollWather());
    }
}
