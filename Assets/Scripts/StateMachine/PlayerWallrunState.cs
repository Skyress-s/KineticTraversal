using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallrunState : PlayerBaseState
{
    private float t = 0f;
    private Context _player;

    public override void EnterState(Context player) {
        _player = player;
        
        
        _player.Wallrun.enabled = true;

        player.IIC._AirMovment.enabled = false;
        player._airDash.enabled = false;

    }
    

    public override void Update(Context player)
    {
        t += Time.deltaTime;
        
        if (!player.IIC.AirTime.b_airTime)
        {
            player.TransitionToState(player.groundState);
            return;
        }

        if (player.Wallrun.WallrunState.CurrentWallrunState == Wallrunning.EWallrunState.exit) {
            player.TransitionToState(player.airState);
            return;
        }
        

    }

    public override void ExitState(Context player)
    {
        
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }
}
