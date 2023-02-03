using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoe : Card
{
    public Tile farmeableTile;

    public override bool play(Tile clickedTile){
        if(clickedTile.GetTileState() == Tile.TileStates.SOIL){
            GridManager.Instance.SetTile(clickedTile.transform.position, farmeableTile);
        }
        
        return true;
    }
}
