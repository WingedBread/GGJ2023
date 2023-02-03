using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickaxe : Card
{
    public override bool play(Tile clickedTile){
        Debug.Log("PICK USED");
        return true;
    }
}
