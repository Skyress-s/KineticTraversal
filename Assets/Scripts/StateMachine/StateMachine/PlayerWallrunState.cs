using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallrunState : PlayerBaseState
{
    public override void EnterState(Context player)
    {
        player.Wallrun2.enabled = true;

        player.IIC._AirMovment.enabled = false;
    }

    public override void Update(Context player)
    {
        Debug.Log("In WallrunState");
<<<<<<< HEAD
        if (!player.IIC.WallrunDetect.wallrunning)
=======
        if (!player.IIC.wallrunning.wallrunning)
>>>>>>> 2530c0655a5d19272e2570d7a60403da8165c159
        {
            player.TransitionToState(player.airState);
        }
    }

    public override void ExitState(Context player)
    {
<<<<<<< HEAD
        player.Wallrun2.currentWallrunState = Wallrunning.WallrunStates.walldetect;
=======
>>>>>>> 2530c0655a5d19272e2570d7a60403da8165c159
    }
}
