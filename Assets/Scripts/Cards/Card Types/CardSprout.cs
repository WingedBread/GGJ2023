using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprout : Card
{
    public override void HideCard()
    {
        gameObject.SetActive(false);
        GameManager.Instance.avaiableSlots[handIndex] = true;
        GameManager.Instance.deck.Add(this);
        GameManager.Instance.DrawCard();
    }
    public override void OnMouseEnter()
    {
        transform.position += Vector3.up * 0.1f;
    }

    public override void OnMouseExit()
    {
        transform.position -= Vector3.up * 0.1f;
    }

    public override void OnMouseDown()
    {
        if (!hasBeenPlayed)
        {
            hasBeenPlayed = true;
        }
        Debug.Log("SPROUT_CARD");
    }

    public override void CardBehaviour()
    {
        //Change to Sprout Tile
    }
    public override void Restart()
    {
        hasBeenPlayed = false;
        GameManager.Instance.avaiableSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}
