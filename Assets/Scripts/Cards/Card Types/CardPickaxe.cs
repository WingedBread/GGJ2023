using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickaxe : Card
{
    public override void HideCard()
    {
        gameObject.SetActive(false);
        GameManager.Instance.avaiableSlots[handIndex] = true;
        GameManager.Instance.UpdateTexts();
        GameManager.Instance.deck.Add(this);
        GameManager.Instance.UpdateTexts();
        GameManager.Instance.DrawCard();
    }

    public override void Restart()
    {
        hasBeenPlayed = false;
        GameManager.Instance.avaiableSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
    public override void OnMouseDown()
    {
        if (!hasBeenPlayed)
        {
            transform.position += Vector3.up * 0.5f;
            hasBeenPlayed = true;
        }
        Debug.Log("PICKAXE_CARD");
    }

    public override void CardBehaviour()
    {

    }
}
