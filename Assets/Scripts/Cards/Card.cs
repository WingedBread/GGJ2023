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

    private Vector3 slotPosition;
    private Vector3 startRotation;
    private Vector3 currentAngle;
    private float rotateSpeed = 1.5f;
    Vector3 endRotation = new Vector3(-20, 194, 0);
    Vector3 starterRotation = new Vector3(-20, 166, 0);
    bool direction = false;

    bool hasBeenSelected = false;

    private void Start()
    {
        startRotation = transform.localEulerAngles;
        currentAngle = startRotation;
        hasBeenSelected = false;
    }

    public abstract bool play(Tile clickedTile);

    public void OnMouseEnter()
    {
        if(!hasBeenSelected) transform.localPosition += Vector3.up * 0.1f;
    }

    public void OnMouseOver()
    {
        if (!hasBeenSelected)
        {
            if (currentAngle.y < 176 && currentAngle.y < 184 && !direction) direction = true;
            if (currentAngle.y > 176 && currentAngle.y > 184 && direction) direction = false;

            if (!direction) RotateToStart();
            else RotateToEnd();
        }

    }
    public void OnMouseExit()
    {
        if (!hasBeenSelected)
        {
            transform.localPosition -= Vector3.up * 0.1f;
            transform.localEulerAngles = startRotation;
        }
    }

    public void OnMouseDown()
    {
        GameManager.Instance.SetClickedCard(this);
        hasBeenSelected = true;
        EnableHighLight(true);
    }

    public void HideCard()
    {
        EnableHighLight(false);
        gameObject.SetActive(false);
    }

    public void EnableHighLight(bool enable)
    {
        if (hasBeenSelected)
        {
            if (enable)
            {
                transform.localPosition += Vector3.up * 0.1f;
                transform.localEulerAngles = startRotation;
                hasBeenSelected = true;
            }
            else
            {
                transform.localPosition -= Vector3.up * 0.1f;
                transform.localEulerAngles = startRotation;
                hasBeenSelected = false;
            }
        }
        //highlight.SetActive(enable);
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

    public void UnClicked()
    {
        EnableHighLight(false);
        transform.position = slotPosition;
    }


    private void RotateToStart()
    {
        currentAngle = new Vector3(
                 Mathf.LerpAngle(currentAngle.x, starterRotation.x, Time.deltaTime * rotateSpeed),
                 Mathf.LerpAngle(currentAngle.y, starterRotation.y, Time.deltaTime * rotateSpeed),
                 Mathf.LerpAngle(currentAngle.z, starterRotation.z, Time.deltaTime * rotateSpeed));

        transform.eulerAngles = currentAngle;
    }

    private void RotateToEnd()
    {
        currentAngle = new Vector3(
                 Mathf.LerpAngle(currentAngle.x, endRotation.x, Time.deltaTime * rotateSpeed),
                 Mathf.LerpAngle(currentAngle.y, endRotation.y, Time.deltaTime * rotateSpeed),
                 Mathf.LerpAngle(currentAngle.z, endRotation.z, Time.deltaTime * rotateSpeed));

        transform.eulerAngles = currentAngle;
    }

    public bool IsCardSelected()
    {
        return hasBeenSelected;
    }

    public void SetSlotPosition(Vector3 pos)
    {
        slotPosition = pos;
    }
}
