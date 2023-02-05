using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;
 
    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }
 
    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
 
        text.text = Mathf.RoundToInt(CalculateFps()).ToString();
    }
 
    private float CalculateFps()
    {
        float total = 0f;
        foreach(float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }
}
