using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Card> deck;
    public List<Card> allCards;
    private List<BoxCollider> cardColliders = new List<BoxCollider>();
    public bool[] avaiableSlots;
    public Transform[] cardSlots;
    public int handSize = 5;

    public void Start()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            cardColliders.Add(allCards[i].GetComponent<BoxCollider>());
        }

        for(int i = 0; i <handSize; i++){
            DrawCard();
        }

        EnableCardCollider(false);
    }

    public void DrawCard()
    {
        if(deck.Count >= 1)
        {
            Card randomCard = deck[Random.Range(0, deck.Count)];
            AudioController.Instance.PlayDrawCardSound();

            for (int i = 0; i < avaiableSlots.Length; i++)
            {
                if(avaiableSlots[i] == true)
                {
                    randomCard.gameObject.SetActive(true);
                    randomCard.transform.position = cardSlots[i].position;
                    avaiableSlots[i] = false;
                    randomCard.SetHandIndex(i);
                    deck.Remove(randomCard);
                    return;
                }
            }
        }
    }

    public void cardUsed(Card card) {
        card.HideCard();
        deck.Add(card);
        card.transform.position = new Vector3(0, 0, 0);
        avaiableSlots[card.getHandIndex()] = true;
        DrawCard();
    }

    public void EnableCardCollider(bool enable)
    {
        for (int i = 0; i < cardColliders.Count; i++)
        {
            cardColliders[i].enabled = enable;
        }
        GameManager.Instance.SetCardsCollidersState(enable);
    }
}