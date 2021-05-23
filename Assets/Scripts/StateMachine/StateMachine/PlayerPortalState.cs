using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPortalState : PlayerBaseState
{
    private Rigidbody rb;
    public Vector3 onEnterVeclocity;

    public override void EnterState(Context player)
    {
        player.IIC._AirMovment.enabled = false;
        player.IIC.WallrunDetect.enabled = false;
        player.Wallrun2.enabled = false;

        rb = player.playerGO.GetComponent<Rigidbody>();
        onEnterVeclocity = rb.velocity;
        Debug.Log(onEnterVeclocity);

        if (player.IIC.grapplingHookStates.currentState == GrapplingHookStates.GHStates.rest) { }
        else player.IIC.grapplingHookStates.ReturningMiddleStep();
    
    }

    public override void Update(Context player)
    {

        //aligs player to center of portal
        player.playerGO.transform.position = PortalMain.centerOfPortal;
        //resets velocity
        rb.velocity = Vector3.zero;

        if (player.IIC.controls.Player.Jump.triggered)
        {
            //what to do when player is ready to travel further
            //
            Context.connectedPortal.currentState = PortalMain.State.dorment;
            Context.connectedPortal.isTriggerd = false;
            PortalMain.staticIsTriggerd = false;

            var exitVelocity = Camera.main.transform.forward * onEnterVeclocity.magnitude;
            rb.velocity = exitVelocity;
            player.TransitionToState(player.airState);

        }
    }

    public override void ExitState(Context player)
    {
        onEnterVeclocity = Vector3.zero;
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }

}
