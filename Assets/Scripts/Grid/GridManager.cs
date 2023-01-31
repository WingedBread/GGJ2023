using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int _width, _height;
    [SerializeField]
    private Tile tileChildA, tileChildB;
    [SerializeField]
    private Transform cam;

    private Dictionary<Vector2, Tile> tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3 ? tileChildB : tileChildA;
                var spawnedTile = Instantiate(randomTile, new Vector3(x,y), Quaternion.identity);
                
                if(randomTile == tileChildA) randomTile.name = $"TileChildA {x} {y}";
                else if (randomTile == tileChildB) randomTile.name = $"TileChildB {x} {y}";
                else randomTile.name = $"Tile {x} {y}";

                spawnedTile.Init(x, y);
                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        cam.transform.position = new Vector3((float)_width / 2 -0.5f, (float)_height / 2 -1.5f, -10.8f);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
