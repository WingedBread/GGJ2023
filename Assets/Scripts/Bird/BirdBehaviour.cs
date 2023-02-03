using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : EndTurnObserver
{
    SpriteRenderer spriteRenderer;
    GridManager gridManager;
    enum Moves {UP, LEFT, RIGHT, DOWN}

    [SerializeField]
    private Tile tileSoil;

    int maxPositionX;
    int maxPositionY;

    void Start()
    {
        gridManager = GameObject.Find("GRID MANAGER").GetComponent<GridManager> ();;
        transform.position = GetInitialPosition();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        maxPositionX = 19;
        maxPositionY = 9;
        EndTurnSubscribe();
    }

    public void Move() 
    {
        List<Moves> possibleMoves = CalculateProsibleMoves();

        if(possibleMoves.Count == 0){
            return;
        }
        Debug.Log(possibleMoves.Count);
        Moves move = possibleMoves[Random.Range(0, possibleMoves.Count)];

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
    }

    List<Moves> CalculateProsibleMoves()
    {
        List<Moves> possibleMoves = new List<Moves>();
        Tile currentTile = gridManager.GetTileAtPosition(transform.position + new Vector3(-1,0,0));

        if(transform.position.x > 0 && gridManager.GetTileAtPosition(transform.position + new Vector3(-1,0,0)).GetTileState() != Tile.TileStates.ROCK){
            possibleMoves.Add(Moves.LEFT);
        } 
        if(transform.position.x < maxPositionX && gridManager.GetTileAtPosition(transform.position + new Vector3(1,0,0)).GetTileState() != Tile.TileStates.ROCK){
            possibleMoves.Add(Moves.RIGHT);
        } 
        if(transform.position.y > 0 && gridManager.GetTileAtPosition(transform.position + new Vector3(0,-1,0)).GetTileState() != Tile.TileStates.ROCK){
            possibleMoves.Add(Moves.DOWN);
        } 
        if(transform.position.y < maxPositionY && gridManager.GetTileAtPosition(transform.position + new Vector3(0,1,0)).GetTileState() != Tile.TileStates.ROCK){
            possibleMoves.Add(Moves.UP);
        }
        return possibleMoves;
    }

    Vector3 GetInitialPosition(){
        List<Vector2> possiblePositions = gridManager.GetSoilTilesPositions();
        if(possiblePositions.Count == 0 ) {
            return new Vector2(-5,-5);
        }
        return possiblePositions[Random.Range(0, possiblePositions.Count)];
    }

    public override bool notify()
    {
        if(Input.GetMouseButtonDown(0)){
            Move();
        }
        Eat();
        return true;
    }

    void Eat()
    {
        Tile currentTile = gridManager.GetTileAtPosition(transform.position);

        if(currentTile.GetTileState() == Tile.TileStates.SPROUT || currentTile.GetTileState() == Tile.TileStates.CARROT){
            gridManager.SetTile(transform.position, tileSoil);
        }
    }

    void onDestroy(){
        EndTurnUnsuscribe();
    }
}
