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

    new void Start(){
        base.Start();
        GameManager.Instance.EndTurnSubscribe(this);
    }
    
    public void notify(){
        growCount ++;

        if(growCount >= growTurn ){
            GridManager.Instance.SetTile(transform.position, tileCarrot);
        }
    }

    public void OnDestroy(){
        GameManager.Instance.EndTurnUnsuscribe(this);
    }
}
