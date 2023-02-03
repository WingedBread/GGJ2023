using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShovel : Card
{
   public override bool play(Tile clickedTile){
    Debug.Log("SHOVEL USED");
    return true;
   }
}
