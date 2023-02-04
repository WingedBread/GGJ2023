using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public enum TileStates { ROCK, SOIL, SOIL_FARMABLE, SOIL_FARMABLE_WET, SPROUT, SPROUT_WET, CARROT }

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject highlight;

    protected TileStates tileState;

    public bool isOccuped = false;

    public bool isProtected = false;

    private GameObject bird;

    public void Start(){
        name = "Tile " + tileState.ToString() + " {" + transform.position.x.ToString() + ", " + transform.position.y + "}";
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    public void OnMouseDown(){
        GameManager.Instance.SetClickedTile(this);
    }

    public void CloseHighlight()
    {
        highlight.SetActive(false);
    }

    public TileStates GetTileState()
    {
        return tileState;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public void setBird(GameObject bird){
        this.bird = bird; 
    }

    public GameObject getBird(){
        return bird;
    }

    public void UnClicked(){

        highlight.SetActive(false);
    }

    public void setOcuped(bool ocuped){
        isOccuped = ocuped;
    }

    public bool getOcuped(){
        return isOccuped;
    }

    public void setProtection(bool protection){
        isProtected = protection;
    }

    public bool getProtection(){
        return isProtected;
    }
}
