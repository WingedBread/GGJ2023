using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IPointerDownHandler
{
    private CardGenerator.CARD_TYPE cardType;

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
                case CardGenerator.CARD_TYPE.SPROUT:
                    CardSproutBehaviour();
                    break;
                case CardGenerator.CARD_TYPE.SPRINKLER:
                    break;
                case CardGenerator.CARD_TYPE.PICKAXE:
                    break;
                case CardGenerator.CARD_TYPE.HOE:
                    break;
                case CardGenerator.CARD_TYPE.SHOVEL:
                    break;
                case CardGenerator.CARD_TYPE.SHOTGUN:
                    break;
                case CardGenerator.CARD_TYPE.SCARECROW:
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

    public void CardSproutBehaviour()
    {
        //if (GetClickedTile().GetTileState() == Tile.TileStates.SOIL_FARMABLE)
        //{
        //    //Destroy and Instantiate new Tile
        //    gridManager.SetTile(GetClickedTile().GetPosition(), )
        //}
        //else
        //{
        //    currentcard.HideCard();
        //}
    }

    public CardGenerator.CARD_TYPE GetCardType()
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
