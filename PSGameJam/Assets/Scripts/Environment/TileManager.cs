using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tilledSoilPrefab;
    [SerializeField]
    private List<String> tillableTiles; 

    private static Tilemap groundMap;
    private static GameObject tilledSoil;
    private static List<Vector3Int> spawnableTiles;

    private static List<String> staticTillableTiles;
    private static Dictionary<Vector3Int, Tile> removedTiles;

    private void Awake()
    {
        tilledSoil = tilledSoilPrefab;
        staticTillableTiles = tillableTiles;

        removedTiles = new Dictionary<Vector3Int, Tile>();
        spawnableTiles = new List<Vector3Int>();
        foreach (var pos in groundMap.cellBounds.allPositionsWithin)
        {
            var t = groundMap.GetTile(pos);

            if (t != null && tillableTiles.Contains(t.name))
            {
                spawnableTiles.Add(pos);
            }
        }
    }

    public static void LaserTile(Vector2 pos)
    {
        Vector3Int grid_pos = groundMap.WorldToCell(pos);
        spawnableTiles.Remove(grid_pos);

        Tile tb = groundMap.GetTile<Tile>(grid_pos);


        //Check if we are hitting anything worth tilling
        if (tb != null && staticTillableTiles.Contains(tb.name))
        {
            removedTiles.Add(grid_pos, tb);
            Tile t = ScriptableObject.CreateInstance<Tile>();
            t.gameObject = tilledSoil;
            groundMap.SetTile(grid_pos, t) ;
        }
    }

    public static void SeedTile(Vector2 pos)
    {
        Vector3Int grid_pos = groundMap.WorldToCell(pos);

        GameObject t = groundMap.GetInstantiatedObject(grid_pos);
        
        if(t != null && t.tag == "Soil")
        {
            t.GetComponent<SoilLogic>().AddSeed();
        }
    }

    public static void ResetTile(Vector2 pos)
    {
        Vector3Int grid_pos = groundMap.WorldToCell(pos);
        spawnableTiles.Add(grid_pos);

        groundMap.SetTile(grid_pos, removedTiles[grid_pos]);
        removedTiles.Remove(grid_pos);
    }

    public static void SetTileMap(Tilemap map)
    {
        groundMap = map;
    }

    public static Vector2 rotate(Vector2 v, float delta)
    {
        float rad = Mathf.Deg2Rad * delta;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
    
    public static Vector3 GetSpawnableRandomPosition()
    {
        if(spawnableTiles.Count == 0) 
            return Vector3.zero;
        return groundMap.CellToWorld(spawnableTiles[UnityEngine.Random.Range(0, spawnableTiles.Count - 1)]);
    }
}
