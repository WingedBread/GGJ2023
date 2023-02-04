using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprout : Tile
{
    new void Start(){
        tileState = TileStates.SPROUT;
        base.Start();
    }
}
