using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public List<Card> deck;
    public List<Card> allCards;
    private List<BoxCollider> cardColliders = new List<BoxCollider>();
    public bool[] avaiableSlots;
    public Transform[] cardSlots;
    public int handSize = 5;

    private Card lastCardUsed;


    public void Start()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            cardColliders.Add(allCards[i].GetComponent<BoxCollider>());
        }

        for(int i = 0; i <handSize; i++){
            DrawCard(true);
        }

        EnableCardCollider(false);
    }

    public void DrawCard(bool start)
    {
        if(deck.Count >= 1)
        {
            Card randomCard = deck[Random.Range(0, deck.Count)];
            if (!start)
            {
                List<Card> filterDeck;
                filterDeck = deck.Where(findCard => findCard.GetCardName() != lastCardUsed.GetCardName()).ToList();

                if(filterDeck.Count <= 0)
                {
                    randomCard = deck[Random.Range(0, deck.Count)];
                }
                else randomCard = filterDeck[Random.Range(0, filterDeck.Count)];
            }

            //AudioController.Instance.PlayDrawCardSound();

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
        lastCardUsed = card;
        deck.Add(card);
        card.transform.position = new Vector3(0, 0, 0);
        avaiableSlots[card.getHandIndex()] = true;
        DrawCard(false);
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