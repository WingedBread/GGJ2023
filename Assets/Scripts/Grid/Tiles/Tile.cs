using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileStates { ROCK, SOIL, SOIL_FARMABLE, SPROUT, CARROT }
    public TileStates tileState;

    //[SerializeField]
    //private Color baseColor, offsetColor;

    public bool wet;
    public bool isPassable;

    [SerializeField]
    private Sprite rockSprite, soilSprite, soilFarmableSprite, sproutSprite, sproutWetSprite, carrotSprite;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject highlight;

    bool hasBeenSelected = false;

    //public void Init(int x, int y)
    //{
    //    //Cambio de Color
    //    //if (tileState == TileStates.SOIL)
    //    //{
    //    //    var isOffset = (x + y) % 2 == 1;
    //    //    spriteRenderer.color = isOffset ? offsetColor : baseColor;
    //    //}
    //}

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }


    private void OnMouseDown()
    {
        GameManager.Instance.SetClickedTile(this);
        Debug.Log(this.name);
        hasBeenSelected = true;
    }

    private void OnMouseUp()
    {
        hasBeenSelected = false;
    }

    public void ChangeTileState(TileStates state)
    {
        tileState = state;

        switch (state)
        {
            case TileStates.ROCK:
                RockTileBehaviour();
                break;
            case TileStates.SOIL:
                SoilTileBehaviour();
                break;
            case TileStates.SOIL_FARMABLE:
                SoilFarmableTileBehaviour();
                break;
            case TileStates.SPROUT:
                if (!wet) SproutTileBehaviour();
                else SproutWetTileBehaviour();
                break;
            case TileStates.CARROT:
                CarrotTileBehaviour();
                break;
        }
    }

    private void RockTileBehaviour()
    {
        spriteRenderer.sprite = rockSprite;
    }
    private void SoilTileBehaviour()
    {
        spriteRenderer.sprite = soilSprite;
    }
    private void SoilFarmableTileBehaviour()
    {
        spriteRenderer.sprite = soilFarmableSprite;
    }

    private void SproutTileBehaviour()
    {
        spriteRenderer.sprite = sproutSprite;
    }
    private void SproutWetTileBehaviour()
    {
        spriteRenderer.sprite = sproutWetSprite;
        //En 3 turnos (cartas jugadas) cambia a Carrot
    }

    private void CarrotTileBehaviour()
    {
        spriteRenderer.sprite = carrotSprite;
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
