using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerBaseState
{
    public float t = 0f;
    private float wallrunCooldown;
    private float portalCooldown;
    public override void EnterState(Context player)
    {
        t = 0f;
        wallrunCooldown = 0f;
        portalCooldown = 0.3f;

        if (player.stateStack[1] == player.wallrunState.ToString())
        {
            wallrunCooldown = 0.45f;
        }



        player.IIC._AirMovment.enabled = true;
        player.IIC.WallrunDetect.enabled = true;

        player.Wallrun2.enabled = false;
        player.IIC.GroundMovment.enabled = false;
        player.IIC.infoSliding.enabled = false;
    }

    public override void Update(Context player)
    {
        t += Time.deltaTime;


        if (!player.IIC.AirTime.b_airTime && t > 0.2f)
        {
            player.TransitionToState(player.groundState);
        }


        if (player.IIC.WallrunDetect.detected && t > wallrunCooldown)
        {
            Debug.Log("Wallrun");
            player.TransitionToState(player.wallrunState);
        }


        if (PortalMain.staticIsTriggerd)
        {
            if (player.stateStack[1] == player.portalState.ToString())
            {
                if (t > portalCooldown)
                {
                    player.TransitionToState(player.portalState);
                }
            }
            else
            {
                player.TransitionToState(player.portalState);
            }
            //Debug.Log("Ready to tranition to portalState");
        }
    }

    public override void ExitState(Context player)
    {
        //Debug.Log("Exiting Air State");
        //player._script2.enabled = false;
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }
}
