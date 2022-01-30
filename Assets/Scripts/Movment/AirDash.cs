using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirDash : MonoBehaviour
{

    public Rigidbody rb;

    public InputInfoCenter IIC;
    // Start is called before the first frame update
    int maxDashes = 3;
    public int dashesLeft= 3;

    public static UnityAction<int> testAction;

    void Start()
    {
        
    }

    void DoDash() {
        Vector3 dir = Camera.main.transform.forward;
        float mag = rb.velocity.magnitude;

        rb.velocity = dir * mag;
        dashesLeft--;
    }

    // Update is called once per frame
    void Update()
    {
        if (IIC.Context.stateStack[1] == IIC.Context.wallrunState.ToString()) { // if previous state was wallrun

        }
        if (IIC.controls.Player.Jump.triggered && dashesLeft > 0) {
            DoDash();
            if (testAction != null)
            {
                testAction.Invoke(dashesLeft);
            }
        }

        if (IIC.grounded._isgrounded)
        {
            dashesLeft = 3;
            if (testAction != null)
            {
                testAction.Invoke(dashesLeft);
            }

        }

        //if (IIC.grounded._isgrounded) {
        //    dashesLeft = maxDashes;
        //}
    }
}
