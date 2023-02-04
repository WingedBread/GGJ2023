using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    public static GridManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("FALTA GAME MANAGER");
            }
            return _instance;
        }
    }

    [SerializeField]
    private int _width, _height;
    [SerializeField]
    private Tile tileSoil, tileRock;
    [SerializeField]
    private Transform cam;

    private Dictionary<Vector2, Tile> tiles;

    public List<BoxCollider2D> tileColliders = new List<BoxCollider2D>();

    [SerializeField]
    Transform tilesParent;

    private void Awake()
    {
        _instance = this;
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

                tiles[new Vector2(x, y)] = placedTile;
                tileColliders.Add(placedTile.GetComponent<BoxCollider2D>());
            }
        }
        //cam.transform.position = new Vector3((float)_width / 2 -0.5f, (float)_height / 2 -2.8f, -10.8f);
    }

    public List<Vector2> GetSoilTilesPositionsAndNotOcupedAndNotProtected(){
        return tiles.Where(tile => tile.Value.GetTileState() == Tile.TileStates.SOIL && !tile.Value.getProtection() && !tile.Value.getOcuped())
            .ToDictionary(pair => pair.Key, pair=> pair.Value).Keys.ToList();
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
        if(!tiles[position].getOcuped()){
            Destroy(tiles[position].gameObject);
            Tile placedTile = Instantiate(tile, position, Quaternion.identity, tilesParent);
            tiles[position] = placedTile;
        }
    }

    public void BirdChangeTile(Vector2 position, Tile tile){
            Destroy(tiles[position].gameObject);
            Tile placedTile = Instantiate(tile, position, Quaternion.identity, tilesParent);
            tiles[position] = placedTile;
    }

    public void EnableGridColliders(bool enable)
    {
        foreach(KeyValuePair<Vector2, Tile> pair in tiles){
            pair.Value.GetComponent<BoxCollider2D>().enabled = enable;
        }
    }

    public void OcupeTile(Vector2 position){
        tiles[position].setOcuped(true);
    }

    public void UnocupeTile(Vector2 position){
        tiles[position].setOcuped(false);
    }
}