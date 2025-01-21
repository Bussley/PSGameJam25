using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap setGroundMap;
    [SerializeField]
    private GameObject tilledSoilPrefab;

    private static Tilemap groundMap;
    private static GameObject tilledSoil;

    private void Awake()
    {
        groundMap = setGroundMap;
        tilledSoil = tilledSoilPrefab;
    }

    public static void LaserTile(Vector2 pos)
    {
        Vector3Int grid_pos = groundMap.WorldToCell(pos);

        TileBase t = groundMap.GetTile(grid_pos);

        //Check if we are hitting anything worth tilling
        if (t != null && t.name == "Terrain_01_8")
        {
            groundMap.SetTile(grid_pos, new Tile() { gameObject = tilledSoil });
        }
    }

    public static void SeedTile(Vector2 pos)
    {
        Vector3Int grid_pos = groundMap.WorldToCell(pos);

        GameObject t = groundMap.GetInstantiatedObject(grid_pos);
        
        if(t != null && t.tag == "Soil")
        {

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
