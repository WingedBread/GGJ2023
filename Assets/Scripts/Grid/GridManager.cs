using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int _width, _height;
    [SerializeField]
    private Tile tileSoil, tileRock;
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
                Tile spawnedTile = tileSoil;

                if (x == 0 || y == 0 || x == _width-1 || y == _height - 1)
                {
                    spawnedTile = tileRock;
                }
                else if(x <= 2 || y <= 2|| x >= _width-3|| y >= _height-3)
                {
                    spawnedTile = Random.Range(0, 6) == 3 ? tileSoil : tileRock;
                }

                Tile placedTile = Instantiate(spawnedTile, new Vector3(x,y), Quaternion.identity);
                
                if(spawnedTile == tileSoil) spawnedTile.name = $"TileSoil {x} {y}";
                else if (spawnedTile == tileRock) spawnedTile.name = $"TileRock {x} {y}";
                else spawnedTile.name = $"Tile {x} {y}";

                placedTile.Init(x, y);
                tiles[new Vector2(x, y)] = placedTile;
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
