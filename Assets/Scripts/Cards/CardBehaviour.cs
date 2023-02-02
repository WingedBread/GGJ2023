using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IPointerDownHandler
{
    private Card.CARD_TYPE cardType;

    public bool hasBeenPlayed = false;

    public int handIndex;

    void Awake()
    {
        cardType = GetComponent<CardDisplay>().card.cardType;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!hasBeenPlayed)
        {
            transform.position += Vector3.up * 50;
            hasBeenPlayed = true;

            switch (cardType)
            {
                case Card.CARD_TYPE.SPROUT:
                    GameManager.Instance.CardSproutBehaviour(this);
                    break;
                case Card.CARD_TYPE.SPRINKLER:
                    break;
                case Card.CARD_TYPE.PICKAXE:
                    break;
                case Card.CARD_TYPE.HOE:
                    break;
                case Card.CARD_TYPE.SHOVEL:
                    break;
                case Card.CARD_TYPE.SHOTGUN:
                    break;
                case Card.CARD_TYPE.SCARECROW:
                    break;
            }
        } 
    }

    public void HideCard()
    {
        gameObject.SetActive(false);
        GameManager.Instance.avaiableSlots[handIndex] = true;
        GameManager.Instance.UpdateTexts();
        GameManager.Instance.deck.Add(this);
        GameManager.Instance.UpdateTexts();
        GameManager.Instance.DrawCard();
    }

    public Card.CARD_TYPE GetCardType()
    {
        return cardType;
    }

    public void Restart()
    {
        hasBeenPlayed = false;
        GameManager.Instance.avaiableSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}
