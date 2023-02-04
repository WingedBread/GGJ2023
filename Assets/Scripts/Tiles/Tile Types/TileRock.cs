using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRock : Tile
{
    new void Start(){
        tileState = TileStates.ROCK;
        base.Start();
    }
}
