using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfoCenter : MonoBehaviour
{
    public IsGrounded grounded;

    public GrapplingHookStates grapplingHookStates;

    public Sliding infoSliding;

    public GameObject hook;

    public Vector2 input;

    public Vector3 worldInput;

    public AirTime AirTime;

    void Update()
    {
        //gets the input
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //calculates the worldinput with y=0

        var cameraTrans = Camera.main.transform;
        //defines camera forward and right with y = 0
        var cameraForward = cameraTrans.forward;
        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        var cameraRight = cameraTrans.right;
        cameraRight.y = 0f;
        cameraRight = cameraRight.normalized;

        var wi = cameraRight * input.x + cameraForward * input.y;
        worldInput = wi.normalized;

        //finds the time since last jump
    }
}
