using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
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

    public void EnableHighLight()
    {
        highlight.SetActive(true);
    }

    public void DisableHighlight()
    {
        highlight.SetActive(false);
    }

    private void OnMouseUp()
    {
        GameManager.Instance.ChangeTileColliderState(true);
        GameManager.Instance.ChangeCardColliderState(false);
    }

    public virtual void CardBehaviour()
    {

    }
}
