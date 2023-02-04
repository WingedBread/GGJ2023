using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSoil : Tile
{
    new void Start(){
        tileState = TileStates.SOIL;
        base.Start();
    }
}
