using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCarrot : Tile
{
    new void Start(){
        tileState = TileStates.CARROT;
        base.Start();
    }

}
