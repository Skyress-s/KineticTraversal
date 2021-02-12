using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfoCenter : MonoBehaviour
{
    public IsGrounded grounded;

    public GrapplingHookStates grapplingHookStates;

    public Sliding infoSliding;

    public Vector2 input;

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        
    }
}
