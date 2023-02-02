using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRock : Tile
{
    public override void OnMouseDown()
    {
        GameManager.Instance.SetClickedTile(this);
        hasBeenSelected = true;
        GameManager.Instance.PlayCard();
    }

    public override void OnMouseUp()
    {
        GameManager.Instance.ChangeTileColliderState(false);
        GameManager.Instance.ChangeCardColliderState(true);
        hasBeenSelected = false;
    }
}
