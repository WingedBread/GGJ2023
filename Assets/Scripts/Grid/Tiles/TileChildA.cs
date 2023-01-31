using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChildA : Tile
{
    [SerializeField]
    private Color baseColor, offsetColor;

    public override void Init(int x, int y)
    {
        var isOffset = (x + y) % 2 == 1;
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }
}
