﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerBaseState
{
    public float t = 0f;
    public override void EnterState(Context player)
    {
        t = 0f;

        //Debug.Log("Entering AirState...");

        player.IIC._AirMovment.enabled = true;
        player.IIC.wallrunning.enabled = true;

        player.Wallrun2.enabled = false;
        player.IIC.GroundMovment.enabled = false;
        player.IIC.infoSliding.enabled = false;
    }

    public override void Update(Context player)
    {
        Debug.Log("In AirStatee");
        t += Time.deltaTime;
        if (!player.IIC.AirTime.b_airTime && t > 0.2f)
        {
            player.TransitionToState(player.groundState);
        }

        if (player.IIC.wallrunning.wallrunning)
        {
            player.TransitionToState(player.wallrunState);
        }
    }

    public override void ExitState(Context player)
    {
        //Debug.Log("Exiting Air State");
        //player._script2.enabled = false;
    }
}
