using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public enum CardNames { SPROUT, SPRINKLER, PICKAXE, HOE, SHOVEL, SHOTGUN, SCARECROW}
    public CardNames cardName;
    [SerializeField]
    private GameObject highlight;

    public Tile tileToChange;

    public bool hasBeenPlayed = false;

    public int handIndex;

    public virtual void HideCard()
    {

    }

    public virtual void Restart()
    {

    }

    public virtual void OnMouseEnter()
    {
        
    }

    public virtual void OnMouseExit()
    {
        
    }

    public virtual void OnMouseDown()
    {
        
    }

    public void EnableHighLight(bool enable)
    {
        highlight.SetActive(enable);
    }

    private void OnMouseUp()
    {
        GameManager.Instance.ChangeTileColliderState(true);
        GameManager.Instance.ChangeCardColliderState(false);
    }

    public virtual void CardBehaviour()
    {
        GameManager.Instance.gridManager.SetTile(GameManager.Instance.GetClickedTile().GetPosition(), tileToChange);
    }

    public CardNames GetCardName()
    {
        return cardName;
    }
}
