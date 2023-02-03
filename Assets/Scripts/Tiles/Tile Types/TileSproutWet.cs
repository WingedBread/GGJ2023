using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSproutWet : Tile, EndTurnObserver
{
    private int growCount = 0;
    [SerializeField]
    private int growTurn;
    [SerializeField]
    private Tile tileCarrot;

    public void Start(){
        tileState = TileStates.SPROUT_WET;
        GameManager.Instance.EndTurnSubscribe(this);
    }
    
    public bool notify(){
        growCount ++;

        if(growCount >= growTurn ){
            GridManager.Instance.SetTile(transform.position, tileCarrot);
        }
        return true;
    }

    public void onDestroy(){
        GameManager.Instance.EndTurnUnsuscribe(this);
    }
}
