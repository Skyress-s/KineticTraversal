using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerBaseState
{
    public override void EnterState(Context player)
    {
        Debug.Log("Entering AirState...");
        player._script2.enabled = true;
    }

    public override void Update(Context player)
    {
        Debug.Log("In AirState");
        if (InputSystem.GetDevice<Keyboard>().spaceKey.wasPressedThisFrame)
        {
            player.TransitionToState(player.groundState);
        }
    }

    public override void ExitState(Context player)
    {
        Debug.Log("Exiting Air State");
        player._script2.enabled = false;
    }
}
