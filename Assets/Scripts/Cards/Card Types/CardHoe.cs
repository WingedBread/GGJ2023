using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoe : Card
{
    public override bool play(Tile clickedTile){
        Debug.Log("HOE USED");
        return true;
    }
}
