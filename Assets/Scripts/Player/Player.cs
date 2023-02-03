using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Card> deck;
    public bool[] avaiableSlots;
    public Transform[] cardSlots;
    public int handSize = 5;

    public void Start(){
        Debug.Log(deck[0]);
        for(int i = 0; i <handSize; i++){
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if(deck.Count >= 1)
        {
            Card randomCard = deck[Random.Range(0, deck.Count)];

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
}