using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField]
    private Slider sliderWater;
    [SerializeField]
    private Slider sliderEarth;
    [SerializeField]
    private Slider sliderSun;
    private List<int> values = new List<int> { 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {

    }
    //-4 a -2 rojo, -1 a 1 es verde, 2 a 4 rojo
    public void UpdateSlidersValue(List<int> powers)
    {
        for (int i = 0; i < values.Count; i++)
        {
            values[i] = values[i] + powers[i];
        }

        sliderWater.value = values[0];
        sliderEarth.value = values[1];
        sliderSun.value = values[2];

        UpdateSliderVisuals(sliderWater);
        UpdateSliderVisuals(sliderEarth);
        UpdateSliderVisuals(sliderSun);

        DetectGameOver();
    }

    void DetectGameOver()
    {
        for (int i = 0; i < values.Count; i++)
        {
            if(values[i] < -4 || values[i] > 4)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    void UpdateSliderVisuals(Slider slider)
    {
        if (slider.value >= 2 && slider.value <= 4)
        {
            slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }

        else if (slider.value <= -2 && slider.value >= -4)
        {
            slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }

        else if (slider.value >= -1 && slider.value <= 1)
        {
            slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.green;
        }
    }

    public List<int> GetCurrentValues()
    {
        return values;
    }

    public void Restart()
    {
        for (int i = 0; i < values.Count; i++)
        {
            values[i] = 0;
        }

        sliderWater.value = values[0];
        sliderEarth.value = values[1];
        sliderSun.value = values[2];

        sliderWater.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.white;
        sliderEarth.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.white;
        sliderSun.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.white;

    }
}
