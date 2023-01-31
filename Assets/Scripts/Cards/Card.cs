using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scriptable Object con los settings de cada carta
[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite art;

    public int powerWater;
    public int powerEarth;
    public int powerSun;
}
