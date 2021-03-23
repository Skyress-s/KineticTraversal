using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    public override void EnterState(Context player)
    {

    }

    public override void Update(Context player)
    {
        Debug.Log("In SlidingState");

        if (!player.IIC.infoSliding.sliding)
        {
            player.TransitionToState(player.groundState);
        }   
    }

    public override void ExitState(Context player)
    {

    }
}
