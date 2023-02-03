using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScarecrow : Card
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
        Debug.Log("SCARECROW_CARD");
    }

    public override void CardBehaviour()
    {

    }
    public override void Restart()
    {
        hasBeenPlayed = false;
        GameManager.Instance.avaiableSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}