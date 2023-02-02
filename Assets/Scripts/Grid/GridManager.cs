using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    Transform tilesParent;

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

                if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
                {
                    spawnedTile = tileRock;
                }
                else if (x <= 2 || y <= 2 || x >= _width - 3 || y >= _height - 3)
                {
                    spawnedTile = Random.Range(0, 6) == 3 ? tileSoil : tileRock;
                }

                Tile placedTile = Instantiate(spawnedTile, new Vector3(x,y), Quaternion.identity, tilesParent);

                if (spawnedTile.GetTileState() == Tile.TileStates.SOIL) spawnedTile.name = $"TileSoil {x} {y}";
                else if (spawnedTile.GetTileState() == Tile.TileStates.ROCK) spawnedTile.name = $"TileRock {x} {y}";
                else spawnedTile.name = $"Tile NONAME {x} {y}";

                tiles[new Vector2(x, y)] = placedTile;
            }
        }
        cam.transform.position = new Vector3((float)_width / 2 -0.5f, (float)_height / 2 -1.5f, -10.8f);
    }

    public List<Vector2> getSoilTilesPositions(){
        return tiles.Where(tile => tile.Value.isSoil()).ToDictionary(pair => pair.Key, pair=> pair.Value).Keys.ToList();
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public void SetTile(Vector2 position, Tile tile)
    {
        Destroy(tiles[position]);
        tiles[position] = tile;
        Instantiate(tile, position, Quaternion.identity);
    }
}