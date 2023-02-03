using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprinkler : Card
{
    public Tile wetTile;
    
    public override bool play(Tile clickedTile){
        if(clickedTile.GetTileState() == Tile.TileStates.SPROUT){
            GridManager.Instance.SetTile(clickedTile.transform.position, wetTile);
        }
        return true;
    }
}
