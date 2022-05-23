using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallrunState : PlayerBaseState
{
    private float t = 0f;

    public override void EnterState(Context player)
    {
        player.Wallrun2.enabled = true;

        player.IIC._AirMovment.enabled = false;
        player.IIC._AirDash.enabled = false;
    }

    public override void Update(Context player)
    {
        t += Time.deltaTime;

        if (!player.IIC.Wallrunning.wallrunning && t > 0.2f /*!player.IIC.WallrunDetect.wallrunning*/)
        {
            player.TransitionToState(player.airState);
        }


        if (!player.IIC.AirTime.b_airTime)
        {
            player.TransitionToState(player.groundState);
        }

    }

    public override void ExitState(Context player)
    {
        //player.Wallrun2.Exit();
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }
}
