using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShotgun : Card
{
   public override bool play(Tile clickedTile){
    Debug.Log("SHOT USED");
    return true;
   }
}
