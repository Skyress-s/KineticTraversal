using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MilkShake;


public class PlayerPortalState : PlayerBaseState
{
    private Rigidbody rb;
    public Vector3 onEnterVeclocity;

    private float stateTime;

    public float maxduration = 2.2f;

    private Context _player;

    public override void EnterState(Context player)
    {
        //enable disable components
        player.IIC._AirMovment.enabled = false;
        player.Wallrun.enabled = false;
        player._airDash.enabled = false;
        
        //sets _player
        _player = player;
        
        //gets rb
        rb = player.playerGO.GetComponent<Rigidbody>();
        onEnterVeclocity = rb.velocity;
        rb.isKinematic = true;

        //reset variables
        stateTime = 0f;

        //events
        PlayerController.instance.Control.Player.Jump.performed += OnJump;

    }

    

    public override void Update(Context player) {
        rb.transform.position = Vector3.Lerp(rb.transform.position, player.activePortal.transform.position, Time.deltaTime * 4f);
    }

    public override void ExitState(Context player)
    {
        PlayerController.instance.Control.Player.Jump.performed -= OnJump;
    }

    public override void DebugState(Context player)
    {
        if (player.debugState) Debug.Log(this);
    }
    
    private void OnJump(InputAction.CallbackContext obj) {
        Debug.LogError("JUMP");
        rb.isKinematic = false;
        rb.velocity = Camera.main.transform.forward * onEnterVeclocity.magnitude ;
        _player.TransitionToState(_player.airState);
    }
    

}
