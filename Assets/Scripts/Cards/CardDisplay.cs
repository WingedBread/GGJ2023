using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script que recoge los datos del scriptable object de la carta y los muestra en el juego, igualando sus valores a los la parte gráfica del juego.
public class CardDisplay : MonoBehaviour
{
    [Header("CHOOSE CARD")]
    public Card card;

    private Image artImage;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        artImage = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        descriptionText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        artImage.sprite = card.art;
        nameText.text = card.name;
        descriptionText.text = card.description;
    }
}
