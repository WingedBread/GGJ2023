using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSoilFarmableWet : Tile
{
    public void Start(){
        tileState = TileStates.SOIL_FARMABLE_WET;
    }
}
