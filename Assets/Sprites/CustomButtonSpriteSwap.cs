using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class CustomButtonSpriteSwap : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField]
    private Sprite idleSprite, selectedSprite, pushSprite;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        button.image.overrideSprite = pushSprite;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        button.image.overrideSprite = selectedSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.overrideSprite = selectedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.overrideSprite = idleSprite;
    }
}
