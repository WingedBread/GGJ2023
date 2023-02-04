using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object: MonoBehaviour
{

    public void OcupeTile(Vector2 position){
        GridManager.Instance.OcupeTile(position);
    }

    public void UnocupeTile(Vector2 position){
        GridManager.Instance.UnocupeTile(position);
    }

}