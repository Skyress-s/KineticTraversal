using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DebugResetPlayer : MonoBehaviour
{
    /*public delegate void restartDelegate();
    public static event restartDelegate RestartLevelEvent;*/
    public static UnityAction ERestartLevelEvent;
    
    public GameObject player;
    
    private Rigidbody rb;

    public InputInfoCenter IIC;

    public GameObject hook;

    private Keyboard kb;


    [Header("give speed debug")]
    public float speedIncrease;

    [Header("Reset CameraRot")]
    public CameraLook camlook;


    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        kb = InputSystem.GetDevice<Keyboard>();

        ResetPlayer();
    }

    private void OnDestroy() {
        ERestartLevelEvent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (kb.rKey.wasPressedThisFrame) {
            ResetPlayer();
        }

#if UNITY_EDITOR
        if (kb.gKey.wasPressedThisFrame) {
            AcceleratePlayer();
        }
#endif
    }


    public void AcceleratePlayer()
    {
        var dir = Camera.main.transform.forward;
        rb.velocity = dir * speedIncrease;


    }

    public void ResetPlayer()
    {
        //invokes the restart event
        ERestartLevelEvent?.Invoke();
       
        


        player.transform.position = PlayerBegin.startPos; //where to place the player
        rb.velocity = Vector3.zero;

        //resets the rotation of the camera
        var n = PlayerBegin.startRot.eulerAngles.x;
        if (n > 89.5f)
        {
            n += -360f;
        }
        camlook.xRotation = n;
        camlook.yRotation = PlayerBegin.startRot.eulerAngles.y;

        Debug.Log(PlayerBegin.startRot.eulerAngles);


        IIC.grapplingHookStates.currentState = GrapplingHookStates.GHStates.rest;
        IIC.grapplingHookStates.AnimHooked(false);

        hook.GetComponent<BoxCollider>().enabled = false;

        IIC.Context.TransitionToState(IIC.Context.groundState);
    }
}
