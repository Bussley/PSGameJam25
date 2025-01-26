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

    private GameObject MarketBoard;

    void Awake()
    {
        Instantiate(PlayerPrefab);
        GameObject map = Instantiate(TileMapPrefab);
        TileManager.SetTileMap(map.transform.GetChild(0).GetComponent<Tilemap>());

        GameObject um = Instantiate(UtilityManagerPrefab);

        MarketBoard = GameObject.FindGameObjectWithTag("CropMarketBoard");

        //Add Listeners to day night cycle
        um.GetComponent<EnviromnentManager>().AddEventCall(() => MarketBoard.GetComponent<CropMarketPlaceLogic>().RollMarket());
        um.GetComponent<EnviromnentManager>().AddEventCall(() => um.GetComponent<EnemyManager>().SpawnScarecrow());
        um.GetComponent<EnviromnentManager>().AddEventCall(() => um.GetComponent<WeatherManager>().RollWather());
    }
}
