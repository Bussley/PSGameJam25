using UnityEngine;
using UnityEngine.Tilemaps;

public class InitializeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerPrefab;

    [SerializeField]
    private GameObject TileMapPrefab;

    [SerializeField]
    private GameObject UtilityManagerPrefab;

    void Awake()
    {
        Instantiate(PlayerPrefab);
        GameObject map = Instantiate(TileMapPrefab);
        Instantiate(UtilityManagerPrefab);

        TileManager.SetTileMap(map.transform.GetChild(0).GetComponent<Tilemap>());
    }
}
