using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour, EndTurnObserver
{
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    enum Moves {UP, LEFT, RIGHT, DOWN}

    [SerializeField]
    private Tile tileSoil;

    int maxPositionX;
    int maxPositionY;

    void Start()
    {
        gameManager = GameManager.Instance;
        transform.position = GetInitialPosition();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        maxPositionX = 19;
        maxPositionY = 9;
        gameManager.EndTurnSubscribe(this);
        GridManager.Instance.GetTileAtPosition(transform.position).setOcuped(true);
        GridManager.Instance.GetTileAtPosition(transform.position).setBird(gameObject);
    }

    public void Move() 
    {
        List<Moves> possibleMoves = CalculatePosibleMoves();

        if(possibleMoves.Count == 0){
            return;
        }
        Moves move = possibleMoves[Random.Range(0, possibleMoves.Count)];

        GridManager.Instance.GetTileAtPosition(transform.position).setOcuped(false);
        GridManager.Instance.GetTileAtPosition(transform.position).setBird(null);

        if(move == Moves.UP){
            transform.position += new Vector3(0,1,0);
        } else if(move == Moves.LEFT){
            transform.position += new Vector3(-1,0,0);
            spriteRenderer.flipX = false;
        } else if(move == Moves.RIGHT){
            transform.position += new Vector3(1,0,0);
            spriteRenderer.flipX = true;
        } else if(move == Moves.DOWN){
            transform.position += new Vector3(0,-1,0);
        } 
        GridManager.Instance.GetTileAtPosition(transform.position).setOcuped(true);
        GridManager.Instance.GetTileAtPosition(transform.position).setBird(gameObject);
    }

    List<Moves> CalculatePosibleMoves()
    {
        List<Moves> possibleMoves = new List<Moves>();
        Tile currentTile = GridManager.Instance.GetTileAtPosition(transform.position + new Vector3(-1,0,0));

        if(transform.position.x > 0 && isPosibleMoveToTile(GridManager.Instance.GetTileAtPosition(transform.position + new Vector3(-1,0,0)))){
            possibleMoves.Add(Moves.LEFT);
        } 
        if(transform.position.x < maxPositionX && isPosibleMoveToTile(GridManager.Instance.GetTileAtPosition(transform.position + new Vector3(1,0,0)))){
            possibleMoves.Add(Moves.RIGHT);
        } 
        if(transform.position.y > 0 && isPosibleMoveToTile(GridManager.Instance.GetTileAtPosition(transform.position + new Vector3(0,-1,0)))){
            possibleMoves.Add(Moves.DOWN);
        } 
        if(transform.position.y < maxPositionY && isPosibleMoveToTile(GridManager.Instance.GetTileAtPosition(transform.position + new Vector3(0,1,0)))){
            possibleMoves.Add(Moves.UP);
        }
        Debug.Log(possibleMoves.Count);
        return possibleMoves;
    }

    private bool isPosibleMoveToTile(Tile tile){
        return tile.GetTileState() != Tile.TileStates.ROCK &&
                !tile.getOcuped() &&
                !tile.getProtection();
    }

    Vector3 GetInitialPosition()
    {
        List<Vector2> possiblePositions = GridManager.Instance.GetSoilTilesPositionsAndNotOcupedAndNotProtected();

        if(possiblePositions.Count == 0 ) {
            return new Vector2(-5,-5);
        }
        return possiblePositions[Random.Range(0, possiblePositions.Count)];
    }

    public bool notify()
    {
        Move();
        Eat();
        return true;
    }

    void Eat()
    {
        Tile currentTile = GridManager.Instance.GetTileAtPosition(transform.position);
        if(currentTile.GetTileState() == Tile.TileStates.SPROUT || currentTile.GetTileState() == Tile.TileStates.SPROUT_WET || currentTile.GetTileState() == Tile.TileStates.CARROT){
            GridManager.Instance.BirdChangeTile(transform.position, tileSoil);
        }
    }

    void OnDestroy()
    {
        gameManager.EndTurnUnsuscribe(this);
    }
}
