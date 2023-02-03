using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScarecrow : Card
{
    public override bool play(Tile clickedTile){
        Debug.Log("SCARECROW USED");
        return true;
    }
}
