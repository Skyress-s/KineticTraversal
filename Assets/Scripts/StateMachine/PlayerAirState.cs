using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerBaseState
{
    public float t = 0f;
    private float wallrunCooldown;
    private float portalCooldown;
    Context _player;
    public override void EnterState(Context player) {
        _player = player;
        t = 0f;
        wallrunCooldown = 0f;
        portalCooldown = 0.3f;
        if (player.stateStack[1] == player.wallrunState.ToString())
        {
            wallrunCooldown = 0.45f;
        }


        player.WallDetect.EFoundWall.AddListener(OnWallDetect);

        
        player.IIC._AirMovment.enabled = true;
        
        
        _player.Wallrun.enabled = false;
        player._airDash.enabled = false;
        player.IIC.GroundMovment.enabled = false;
        player.IIC.infoSliding.enabled = false;
    }

    public override void Update(Context player)
    {
        t += Time.deltaTime;

        if (!player.IIC.AirTime.b_airTime && t > 0.2f) {
       
            player.TransitionToState(player.groundState);
            return;
        }

        if (t > 0.2f) {
            player._airDash.enabled = true;
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


        if (player._GrapplingHookStates.currentState == GrapplingHookStates.GHStates.hooked) {
            player._airDash.enabled = false;
        }
        else {
            player._airDash.enabled = true;
        }
    }

    public override void ExitState(Context player)
    {
       _player.WallDetect.EFoundWall.RemoveListener(OnWallDetect);
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }

    private void OnWallDetect() {
        _player.TransitionToState(_player.wallrunState);
    }

    
}
