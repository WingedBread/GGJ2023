using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSoilFarmable : Tile
{
    public void Start(){
        tileState = TileStates.SOIL_FARMABLE;
    }
}
