using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTiltWhileGrappling : MonoBehaviour
{
    public InputInfoCenter IIC;

    private GameObject hook;

    void Start()
    {
        hook = IIC.hook;    
    }

    void Update()
    {
        //if (IIC.grapplingHookStates.currentState.ToString() == "hooked")
        //{
        //    var v = hook.transform.position - transform.position;
        //    v = v.normalized;
        //    // TODO convert to localSpace

        //    var resultVector = Vector3.Lerp(Vector3.up, v, 1f);

        //    Quaternion q = Quaternion.LookRotation(resultVector, Vector3.up);

        //    transform.localRotation = q;

        //    //Debug.DrawRay(transform.position, v*10f, Color.yellow, 1f
        //}
        //else
        //{
        //    transform.localRotation = Quaternion.identity;
        //}

        if (IIC.grapplingHookStates.currentState.ToString() == "hooked")
        {
            var hook = IIC.hook.transform.position - transform.position;
            hook = hook.normalized;

            // finds the world forwards direction
            var up = transform.up;

            var interpelated = Vector3.Lerp(up, hook, 0.1f); //1 is hook and 0 is local Up

            //var q = Quaternion.LookRotation(in)
            Debug.Log(interpelated);

        }
    }
}
