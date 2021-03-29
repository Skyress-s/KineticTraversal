﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public override void EnterState(Context player)
    {
        //Debug.Log("Entering Groundstate...");

        player.IIC.infoSliding.enabled = true;
        player.IIC.GroundMovment.enabled = true;
        
        player.IIC._AirMovment.enabled = false;
        player.IIC.WallrunDetect.enabled = false;
        player.Wallrun2.enabled = false;

    }

    public override void Update(Context player)
    {
        Debug.Log("In GroundState");

        //if (InputSystem.GetDevice<Keyboard>().spaceKey.wasPressedThisFrame)
        //{
        //    player.TransitionToState(player.airState);
        //    //player._script1.enabled = false;
        //}

        if (player.IIC.infoSliding.sliding)
        {
            player.TransitionToState(player.slideState);
        }

        if (player.IIC.AirTime.b_airTime || player.IIC.holdJump)
        {
            player.TransitionToState(player.airState);
            
        }
    }

    public override void ExitState(Context player)
    {
        //Debug.Log("Exiting Ground State");
    }
}
