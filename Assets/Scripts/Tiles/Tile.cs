using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public enum TileStates { ROCK, SOIL, SOIL_FARMABLE, SPROUT, SPROUT_WET, CARROT }
    public TileStates tileState;

    public bool wet;
    public bool isPassable;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject highlight;

    public bool hasBeenSelected = false;

    public virtual void TileBehaviour()
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

    public void CloseHighlight()
    {
        highlight.SetActive(false);
    }


    public virtual void OnMouseDown()
    {

    }

    public virtual void OnMouseUp()
    {

    }

    public TileStates GetTileState()
    {
        return tileState;
    }

    public bool isSoil(){
        return tileState == TileStates.SOIL;
    }

    public bool isConsumible(){
        return tileState == TileStates.SPROUT || tileState == TileStates.CARROT;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public bool HasBeenSelected()
    {
        return hasBeenSelected;
    }
}
