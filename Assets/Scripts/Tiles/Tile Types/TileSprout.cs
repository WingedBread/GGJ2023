using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprout : Tile
{
    public void Start(){
        tileState = TileStates.SPROUT;
    }
/*
    public override void OnMouseUp()
    {
        CloseHighlight();
        GameManager.Instance.ChangeTileColliderState(false);
        GameManager.Instance.ChangeCardColliderState(true);
        hasBeenSelected = false;
        GameManager.Instance.PlayCard();
    }*/
}
