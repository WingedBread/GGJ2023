using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprout : Tile
{
    new void Start(){
        GameManager.Instance.FirstSproutPlaced();
        base.Start();
    }
}
