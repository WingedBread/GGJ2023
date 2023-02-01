using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GridManager gridManager;
    enum Moves {UP, LEFT, RIGHT, DOWN}

    int maxPositionX;
    int maxPositionY;

    void Start()
    {
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager> ();;
        transform.position = getInitialPosition();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        maxPositionX = 19;
        maxPositionY = 9;  
    }

    public void move() 
    {
        List<Moves> possibleMoves = calculateProsibleMoves();
        Moves move = possibleMoves[Random.Range(0, possibleMoves.Count-1)];

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

    List<Moves> calculateProsibleMoves()
    {
        List<Moves> possibleMoves = new List<Moves>();

        if(transform.position.x > 0 ){
            possibleMoves.Add(Moves.LEFT);
        } 
        if(transform.position.x < maxPositionX){
            possibleMoves.Add(Moves.RIGHT);
        } 
        if(transform.position.y > 0){
            possibleMoves.Add(Moves.DOWN);
        } 
        if(transform.position.y < maxPositionY){
            possibleMoves.Add(Moves.UP);
        }
        return possibleMoves;
    }

    Vector3 getInitialPosition(){
        List<Vector2> possiblePositions = gridManager.getSoilTilesPositions();
        return possiblePositions[Random.Range(0, possiblePositions.Count-1)];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            move();
        }
    }
}
