using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scriptable Object con los settings de cada carta
[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardGenerator : ScriptableObject
{
    public enum CARD_TYPE { SPROUT, SPRINKLER, PICKAXE, HOE, SHOVEL, SHOTGUN, SCARECROW}
    public CARD_TYPE cardType;
    public new string name;
    public string description;

    public Sprite art;
}
