using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShotgun : Card
{
   public override bool play(Tile clickedTile){
      GameObject bird = clickedTile.getBird();
        if (bird != null)
        {
            Destroy(bird);
            AudioController.Instance.PlayShotgunOnBirdSound();
        }
        else AudioController.Instance.PlayIncorrectSound();
        return true;
   }
}
