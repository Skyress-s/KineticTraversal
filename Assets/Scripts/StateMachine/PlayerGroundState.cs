using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    
    public override void EnterState(Context player)
    {
        //Debug.Log("Entering Groundstate...");

        player.IIC.infoSliding.enabled = true;

        //adds a little logic to not turn on ground movment if the player is holding slide
        if (!player.IIC.holdSlide)
        {
            player.IIC.GroundMovment.enabled = true;
        }

        
        player.IIC._AirMovment.enabled = false;
       
        player.Wallrun.enabled = false;

        player.IIC.Wallrunning.enabled = false;
        player._airDash.enabled = false;
    }

    public override void Update(Context player)
    {
        

        player._airDash.enabled = false;

        if (player.IIC.holdJump)
        {
            player.TransitionToState(player.airState);
        }
        else if (player.IIC.AirTime.b_airTime == true)
        {
            player.TransitionToState(player.airState);
        }
        else if (player.IIC.holdSlide)
        {
            player.TransitionToState(player.slideState);
        }
    }

    public override void ExitState(Context player)
    {
        //Debug.Log("Exiting Ground State");
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }

}
