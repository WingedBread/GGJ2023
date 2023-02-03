using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoe : Card
{
    public Tile farmeableTile;

    public override bool play(Tile clickedTile){
        GridManager.Instance.Sgit staetTile(clickedTile.transform.position, farmeableTile);
        return true;
    }
}
