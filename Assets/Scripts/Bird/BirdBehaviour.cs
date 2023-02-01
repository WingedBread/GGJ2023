using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    static string UP = "up";
    static string LEFT = "left";
    static string RIGHT = "right";
    static string DOWN = "down";

    int maxPositionX;
    int maxPositionY;

    void Start()
    {
        transform.position = getInitialPosition();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        maxPositionX = 19;
        maxPositionY = 9;  
    }

    public void move() 
    {
        List<string> moves = calculateProsibleMoves();
        string move = moves[Random.Range(0, moves.Count)];

        if(move == UP){
            transform.position += new Vector3(0,1,0);
        } else if(move == LEFT){
            transform.position += new Vector3(-1,0,0);
            spriteRenderer.flipX = false;
        } else if(move == RIGHT){
            transform.position += new Vector3(1,0,0);
            spriteRenderer.flipX = true;
        } else if(move == DOWN){
            transform.position += new Vector3(0,-1,0);
        } 
    }

    List<string> calculateProsibleMoves()
    {
        List<string> possibleMoves = new List<string>();

        if(transform.position.x > 0 ){
            possibleMoves.Add(LEFT);
        } 
        if(transform.position.x < maxPositionX){
            possibleMoves.Add(RIGHT);
        } 
        if(transform.position.y > 0){
            possibleMoves.Add(DOWN);
        } 
        if(transform.position.y < maxPositionY){
            possibleMoves.Add(UP);
        }
        return possibleMoves;
    }

    Vector3 getInitialPosition(){
        return new Vector3(10,5,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            move();
        }
    }
}
