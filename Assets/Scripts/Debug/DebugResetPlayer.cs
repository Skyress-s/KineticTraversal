using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugResetPlayer : MonoBehaviour
{
    public delegate void restartDelegate();
    public static event restartDelegate RestartLevelEvent;



    public GameObject Player;
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
        rb = Player.GetComponent<Rigidbody>();
        kb = InputSystem.GetDevice<Keyboard>();

        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (kb.rKey.wasPressedThisFrame)
        {
            ResetPlayer();
        }

#if UNITY_EDITOR
        if (kb.gKey.wasPressedThisFrame)
        {
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
        try
        {
            RestartLevelEvent.Invoke();
        }
        catch (System.Exception)
        {
            //TODO is this a good solution????
            Debug.Log("did not find any wallobs in the scene");
        }


        Player.transform.position = StartPosForPlayer.startPos; //where to place the player
        rb.velocity = Vector3.zero;

        //resets the rotation of the camera
        var n = StartPosForPlayer.startRot.eulerAngles.x;
        if (n > 89.5f)
        {
            n += -360f;
        }
        camlook.xRotation = n;
        camlook.yRotation = StartPosForPlayer.startRot.eulerAngles.y;

        Debug.Log(StartPosForPlayer.startRot.eulerAngles);


        IIC.grapplingHookStates.currentState = GrapplingHookStates.GHStates.rest;
        IIC.grapplingHookStates.AnimHooked(false);

        hook.GetComponent<BoxCollider>().enabled = false;

        IIC.Context.TransitionToState(IIC.Context.groundState);
    }
}
