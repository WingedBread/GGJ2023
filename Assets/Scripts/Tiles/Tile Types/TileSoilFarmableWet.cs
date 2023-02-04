using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSoilFarmableWet : Tile
{
    new void Start(){
        tileState = TileStates.SOIL_FARMABLE_WET;
        base.Start();
    }
}
