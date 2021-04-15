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
        player.playerGO.transform.position = PortalMain.centerOfPortal;
        if (player.IIC.controls.Player.Jump.triggered || player.IIC.controls.Player.Shoot.triggered)
        {
            //what to do when player is ready to travel further
            var exitVelocity = Camera.main.transform.forward * onEnterVeclocity.magnitude;
            rb.velocity = exitVelocity;
            player.TransitionToState(player.airState);
        }
    }

    public override void ExitState(Context player)
    {
        onEnterVeclocity = Vector3.zero;
    }
}
