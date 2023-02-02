using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScarecrow : Card
{
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
            GameManager.Instance.SetPlayedCard(this);
            //EnableHighLight(true);
        }
    }
    public override void HideCard()
    {
        //EnableHighLight(false);
        gameObject.SetActive(false);
        GameManager.Instance.avaiableSlots[handIndex] = true;
        GameManager.Instance.deck.Add(this);
        GameManager.Instance.DrawCard();
    }

    public override void CardBehaviour()
    {
        //Tienen un radio de influencia de 1 tile a su alrededor
        //en todas las direcciones. Los cuervos no pueden atravesar ese área. Expiran en 6 turnos.
        //Al pasar el ratón por encima se resalta el área de efecto.
    }

    public override void Restart()
    {
        hasBeenPlayed = false;
        GameManager.Instance.avaiableSlots[handIndex] = true;
        gameObject.SetActive(false);
    }
}
