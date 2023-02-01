using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public enum TileStates { ROCK, SOIL, SOIL_FARMABLE, SPROUT, CARROT }
    public TileStates tileState;

    public bool wet;

    [SerializeField]
    private Sprite rockSprite, soilSprite, soilFarmableSprite, sproutSprite, sproutWetSprite, carrotSprite;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;

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

    void RockTileBehaviour()
    {
        spriteRenderer.sprite = rockSprite;
    }
    void SoilTileBehaviour()
    {
        spriteRenderer.sprite = soilSprite;
    }
    void SoilFarmableTileBehaviour()
    {
        spriteRenderer.sprite = soilFarmableSprite;
    }
    
    void SproutTileBehaviour()
    {
        spriteRenderer.sprite = sproutSprite;
    }
    void SproutWetTileBehaviour()
    {
        spriteRenderer.sprite = sproutWetSprite;
    }

    void CarrotTileBehaviour()
    {
        spriteRenderer.sprite = carrotSprite;
    }
}
