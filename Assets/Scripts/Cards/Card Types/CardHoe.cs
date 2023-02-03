using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoe : Card
{
    public Tile farmeableTile;

    public override bool play(Tile clickedTile){
        Debug.Log("HOE USED");
        GridManager.Instance.SetTile(clickedTile.transform.position, farmeableTile);
        return true;
    }
}
