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

    private TextMeshProUGUI powerWaterText;
    private TextMeshProUGUI powerEarthText;
    private TextMeshProUGUI powerSunText;
    // Start is called before the first frame update
    void Start()
    {
        artImage = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        descriptionText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        powerWaterText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        powerEarthText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        powerSunText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        artImage.sprite = card.art;
        nameText.text = card.name;
        descriptionText.text = card.description;
        powerWaterText.text = card.powerWater.ToString();
        powerEarthText.text = card.powerEarth.ToString();
        powerSunText.text = card.powerSun.ToString();
    }
}
