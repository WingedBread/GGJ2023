using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IPointerDownHandler
{
    public bool hasBeenPlayed = false;

    public int handIndex;

    public List<int> power;

    void Awake()
    {
        power = new List<int>();
        power.Add(GetComponent<CardDisplay>().card.powerWater);
        power.Add(GetComponent<CardDisplay>().card.powerEarth);
        power.Add(GetComponent<CardDisplay>().card.powerSun);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!hasBeenPlayed)
        {
            transform.position += Vector3.up * 50;
            hasBeenPlayed = true;
            GameManager.Instance.avaiableSlots[handIndex] = true;
            GameManager.Instance.UpdateSliders(this);
            GameManager.Instance.discardPile.Add(this);
            Invoke("HideCard", 1f);
        } 
    }

    public void HideCard()
    {
        gameObject.SetActive(false);
        GameManager.Instance.UpdateTexts();
    }

    public List<int> GetCardPowers()
    {
        return power;
    }

    public void Restart()
    {
        hasBeenPlayed = false;
        GameManager.Instance.avaiableSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}
