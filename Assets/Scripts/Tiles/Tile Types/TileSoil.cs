using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSoil : Tile
{
    [SerializeField]
    private Sprite[] sprites;

    new void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
