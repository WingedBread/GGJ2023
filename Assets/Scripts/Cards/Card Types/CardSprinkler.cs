using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprinkler : Card
{
    public Tile soilFarmableWetTile;
    public Tile sproutWetTile;

    public override bool play(Tile clickedTile){
        if(clickedTile.GetTileState() == Tile.TileStates.SPROUT){
            GridManager.Instance.SetTile(clickedTile.transform.position, sproutWetTile);
        }
        else if (clickedTile.GetTileState() == Tile.TileStates.SOIL_FARMABLE)
        {
            GridManager.Instance.SetTile(clickedTile.transform.position, soilFarmableWetTile);
        }
        return true;
    }
}
