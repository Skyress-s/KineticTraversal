using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerBaseState
{
    public float stateTime;
    private float wallrunCooldown;
    private float portalCooldown;
    Context _player;
    public override void EnterState(Context player) {
        _player = player;
        stateTime = 0f;
        wallrunCooldown = 0f;
        if (player.stateStack[1] == player.wallrunState.ToString())
        {
            wallrunCooldown = 0.15f;
        }
        portalCooldown = 0.3f;



        
        player.IIC._AirMovment.enabled = true;
        
        
        _player.Wallrun.enabled = false;
        player._airDash.enabled = false;
        player.IIC.GroundMovment.enabled = false;
        player.IIC.infoSliding.enabled = false;
        
        //events
        PortalMain.EEnterPortal.AddListener(OnEnterPortal);
        player.WallDetect.EFoundWall.AddListener(OnWallDetect);
        // PortalMain.EEnterPortalBingus += OnEnterPortal;
    }
    

    public override void Update(Context player)
    {
        stateTime += Time.deltaTime;

        if (!player.IIC.AirTime.b_airTime && stateTime > 0.2f) {
       
            player.TransitionToState(player.groundState);
            return;
        }

        if (stateTime > 0.2f) {
            player._airDash.enabled = true;
        }
        

        //disables airdash
        if (player._GrapplingHookStates.currentState == GrapplingHookStates.GHStates.hooked) {
            player._airDash.enabled = false;
        }
        else {
            player._airDash.enabled = true;
        }
    }

    public override void ExitState(Context player)
    {
       
       //events
       _player.WallDetect.EFoundWall.RemoveListener(OnWallDetect);
       PortalMain.EEnterPortal.RemoveListener(OnEnterPortal);
       // PortalMain.EEnterPortalBingus -= OnEnterPortal;
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }

    private void OnWallDetect() {
        _player.TransitionToState(_player.wallrunState);
    }

    private void OnEnterPortal(PortalMain portalMain) {
        Debug.Log(stateTime + "  " + portalCooldown);
        if (stateTime > portalCooldown) {
             Debug.Log("Should Enter portal!");
            _player.activePortal = portalMain;
            _player.TransitionToState(_player.portalState);
            
        }
        
    }

    
}
