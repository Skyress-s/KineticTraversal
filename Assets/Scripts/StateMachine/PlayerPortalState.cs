using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MilkShake;


public class PlayerPortalState : PlayerBaseState
{
    private Rigidbody rb;
    public Vector3 onEnterVeclocity;

    private float t;

    public static float maxduration = 2.2f;


    public delegate void ExitPortalDelegate();
    public static ExitPortalDelegate ExitPortalEvent;
    public static ExitPortalDelegate EnterPortalEvent;

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


        //events
        EnterPortalEvent.Invoke();
    
    }

    public override void Update(Context player)
    {
        t += Time.deltaTime;

        //aligs player to center of portal
        player.playerGO.transform.position = PortalMain.centerOfPortal;
        //resets velocity
        rb.velocity = Vector3.zero;

        if (player.IIC.controls.Player.Jump.triggered || t > maxduration)
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
        ExitPortalEvent.Invoke();
        t = 0f;
        onEnterVeclocity = Vector3.zero;
        
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }

}
