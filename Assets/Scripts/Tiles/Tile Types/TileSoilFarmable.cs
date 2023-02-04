using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSoilFarmable : Tile
{
    new void Start(){
        tileState = TileStates.SOIL_FARMABLE;
        base.Start();
    }
}
