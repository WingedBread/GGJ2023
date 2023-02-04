using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScarecrow : Card
{
    public GameObject scarecrowPrefab;
    public override bool play(Tile clickedTile){
        if (clickedTile.GetTileState() == Tile.TileStates.SOIL || clickedTile.GetTileState() == Tile.TileStates.SOIL_FARMABLE)
        {
            //Instantiate(scarecrowPrefab,new Vector3 (clickedTile.GetPosition().x, clickedTile.GetPosition().y, 0));
            GameObject scarecrow;
            scarecrow = Instantiate(scarecrowPrefab, transform);
            scarecrow.transform.parent = null;
            scarecrow.transform.rotation = Quaternion.Euler(0, 0, 0);
            scarecrow.transform.localScale = new Vector3(1, 1, 1);
            scarecrow.transform.position = clickedTile.GetPosition();

            Debug.Log("SCARECROW USED");
        }
        return true;
    }
}
