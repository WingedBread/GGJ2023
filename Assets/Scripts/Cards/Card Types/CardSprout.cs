using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprout : Card
{
    public Tile sproutTile;

    public override bool play(Tile clickedTile){
        if(clickedTile.GetTileState() == Tile.TileStates.SOIL_FARMABLE){
            GridManager.Instance.SetTile(clickedTile.transform.position, sproutTile);
        }
        return true;
    }
}
