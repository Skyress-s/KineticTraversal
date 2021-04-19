using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    public float t = 0f;
    public override void EnterState(Context player)
    {
        t = 0f;

        player.IIC.infoSliding.enabled = true;

        player.IIC.GroundMovment.enabled = false;
    }

    public override void Update(Context player)
    {
        t += Time.deltaTime;


        if (!player.IIC.infoSliding.sliding && t > 0.2f)
        {
            player.TransitionToState(player.groundState);
        }

        if (player.IIC.AirTime.b_airTime && t > 0.2f)
        {
            player.TransitionToState(player.airState);
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
