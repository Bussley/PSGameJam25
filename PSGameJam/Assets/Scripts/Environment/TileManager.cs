using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap setMap;
    [SerializeField]
    private GameObject setWheatTile;

    private static Tilemap map;
    private static GameObject wheatTile;

    private void Awake()
    {
        map = setMap;
        wheatTile = setWheatTile;
    }

    public static void ChangeTile(Vector2 pos)
    {
        Vector3Int grid_pos = map.WorldToCell(pos);

        GameObject gameObjectAtPosition = map.GetInstantiatedObject(grid_pos);
        if (gameObjectAtPosition == null)
        {
            map.SetTile(grid_pos, new Tile() { gameObject = wheatTile });
            //Debug.Log("test");
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
