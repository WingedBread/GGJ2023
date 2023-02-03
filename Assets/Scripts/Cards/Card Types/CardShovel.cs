using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShovel : Card
{

   public Tile soilTile;

   public override bool play(Tile clickedTile){
      if(clickedTile.GetTileState() == Tile.TileStates.CARROT){
         GridManager.Instance.SetTile(clickedTile.transform.position, soilTile);
      }

      return true;
   }
}
