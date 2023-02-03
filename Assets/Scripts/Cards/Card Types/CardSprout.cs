using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprout : Card
{
    public override bool play(Tile clickedTile){
        Debug.Log("SPROUT USED");
        return true;
    }
}
