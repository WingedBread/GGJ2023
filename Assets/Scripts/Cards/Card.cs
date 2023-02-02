using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public bool hasBeenPlayed = false;

    public int handIndex;

    public virtual void HideCard()
    {
        gameObject.SetActive(false);
        GameManager.Instance.avaiableSlots[handIndex] = true;
        GameManager.Instance.UpdateTexts();
        GameManager.Instance.deck.Add(this);
        GameManager.Instance.UpdateTexts();
        GameManager.Instance.DrawCard();
    }

    public virtual void Restart()
    {
        hasBeenPlayed = false;
        GameManager.Instance.avaiableSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}
