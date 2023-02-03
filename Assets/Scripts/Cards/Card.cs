using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public enum CardNames { SPROUT, SPRINKLER, PICKAXE, HOE, SHOVEL, SHOTGUN, SCARECROW}
    public CardNames cardName;
    [SerializeField]
    private GameObject highlight;
    private int handIndex;

    
    public abstract bool play(Tile clickedTile);

    public void OnMouseEnter()
    {
        transform.position += Vector3.up * 0.1f;
    }

    public void OnMouseExit()
    {
        transform.position -= Vector3.up * 0.1f;
    }

    public void OnMouseDown()
    {
        GameManager.Instance.PlayCard(this);
    }

    public void HideCard()
    {
        gameObject.SetActive(false);
    }

    public void EnableHighLight(bool enable)
    {
        highlight.SetActive(enable);
    }

    public void Restart()
    {
        gameObject.SetActive(false);
    }

    public CardNames GetCardName()
    {
        return cardName;
    }

    public void SetHandIndex(int i){
        handIndex = i;
    }

    public int getHandIndex(){
        return handIndex;
    }
}
