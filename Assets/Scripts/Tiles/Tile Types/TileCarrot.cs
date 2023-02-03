using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCarrot : Tile
{
    public override void OnMouseDown()
    {
        GameManager.Instance.SetClickedTile(this);
        hasBeenSelected = true;
    }

    public override void OnMouseUp()
    {
        CloseHighlight();
        GameManager.Instance.ChangeTileColliderState(false);
        GameManager.Instance.ChangeCardColliderState(true);
        hasBeenSelected = false;
        GameManager.Instance.PlayCard();
    }
}
