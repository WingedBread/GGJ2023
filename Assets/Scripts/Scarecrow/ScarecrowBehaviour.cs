using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowBehaviour : Object, EndTurnObserver
{
    [SerializeField]
    private GameObject area;

    private List<Vector2> areaLocations = new List<Vector2>();

    private int liveTurns = 0;

    public int maxTurns = 5;

    // Start is called before the first frame update
    void Start()
    {
        FillScarecrowAreaLocation();
        Debug.Log("to protect");
        ProtectTiles();
        Debug.Log("protected");
        OcupeTile(transform.position);
        GameManager.Instance.EndTurnSubscribe(this);
    }

    void FillScarecrowAreaLocation()
    {
        areaLocations.Add(new Vector2(transform.position.x + 1, transform.position.y));
        areaLocations.Add(new Vector2(transform.position.x - 1, transform.position.y));
        areaLocations.Add(new Vector2(transform.position.x, transform.position.y+1));
        areaLocations.Add(new Vector2(transform.position.x, transform.position.y - 1));
        areaLocations.Add(new Vector2(transform.position.x + 1, transform.position.y + 1));
        areaLocations.Add(new Vector2(transform.position.x - 1, transform.position.y - 1));
        areaLocations.Add(new Vector2(transform.position.x + 1, transform.position.y - 1));
        areaLocations.Add(new Vector2(transform.position.x - 1, transform.position.y + 1));
        areaLocations.Add(new Vector2(transform.position.x, transform.position.y));
    }

    public List<Vector2> GetAreaLocations()
    {
        return areaLocations;
    }

    private void ProtectTiles(){
        foreach(Vector2 tilePositions in areaLocations){
            Tile tile = GridManager.Instance.GetTileAtPosition(tilePositions);

            if(tile != null) tile.setProtection(true);
        }
    }

    private void UnprotectTiles(){
        foreach(Vector2 tilePositions in areaLocations){
            Tile tile = GridManager.Instance.GetTileAtPosition(tilePositions);
            if (tile != null) tile.setProtection(false);
        }
    }

    public void notify()
    {
        liveTurns ++;

        if(liveTurns >= maxTurns){
            Destroy(gameObject);
        }
    }

    public void OnDestroy(){
        UnocupeTile(transform.position);
        UnprotectTiles();
        GameManager.Instance.EndTurnUnsuscribe(this);
    }

}
