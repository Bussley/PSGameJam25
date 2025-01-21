using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap setMap;
    [SerializeField]
    private GameObject tilledSoilPrefab;

    private static Tilemap map;
    private static GameObject tilledSoil;

    private void Awake()
    {
        map = setMap;
        tilledSoil = tilledSoilPrefab;
    }

    public static void ChangeTile(Vector2 pos)
    {
        Vector3Int grid_pos = map.WorldToCell(pos);

        //GameObject gameObjectAtPosition = map.GetInstantiatedObject(grid_pos);
        if (map.GetTile(grid_pos).name == "Terrain_01_8")
        {
            map.SetTile(grid_pos, new Tile() { gameObject = tilledSoil });
        }
    }
    public static Vector2 rotate(Vector2 v, float delta)
    {
        float rad = Mathf.Deg2Rad * delta;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
}
