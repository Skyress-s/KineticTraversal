using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfoCenter : MonoBehaviour
{
    public IsGrounded grounded;

    public GrapplingHookStates grapplingHookStates;

    public Sliding infoSliding;

    public Vector2 input;

    public Vector3 worldInput;

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //defines camera forward and right with y = 0
        var cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        var cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        cameraRight = cameraRight.normalized;

        var wi = cameraRight * input.x + cameraForward * input.y;
        worldInput = wi.normalized;
    }
}
