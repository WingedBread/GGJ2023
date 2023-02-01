using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public enum TileStates {ROCK, SOIL, SOIL_FARMABLE,  SPROUT, CARROT}
    public TileStates _states;

    public bool wet;

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
        Debug.Log(this.name);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
