using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprinkler : Card
{
    
    public override bool play(Tile clickedTile){
        Debug.Log("SPRINKLER USED");
        return true;
    }
}
