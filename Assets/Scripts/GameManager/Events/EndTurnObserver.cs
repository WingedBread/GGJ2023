using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EndTurnObserver : MonoBehaviour
{
    GameManager gameManager = GameManager.Instance;

    public void EndTurnSubscribe(){
        gameManager.EndTurnSubscribe(this);
    }

    public abstract bool notify();

    public void EndTurnUnsuscribe(){
        gameManager.EndTurnUnsuscribe(this);
    }
}
