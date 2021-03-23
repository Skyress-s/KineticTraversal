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
        if (!player.IIC.wallrunning.wallrunning)
        {
            player.TransitionToState(player.airState);
        }
    }

    public override void ExitState(Context player)
    {
    }
}
