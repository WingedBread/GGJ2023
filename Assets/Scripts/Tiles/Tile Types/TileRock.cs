using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRock : Tile
{
    [SerializeField]
    private Sprite[] sprites;

    new void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
