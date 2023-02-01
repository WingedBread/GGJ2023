using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{

    public enum TileStates { ROCK, SOIL, SOIL_FARMABLE, SPROUT, CARROT }
    public TileStates tileState;
    public bool isPassable;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject highlight;
    
    public virtual void Init(int x, int y)
    {
        
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    public bool isSoil(){
        return tileState == TileStates.SOIL;
    }

    public bool isConsumible(){
        return tileState == TileStates.SPROUT || tileState == TileStates.CARROT;
    }
}
