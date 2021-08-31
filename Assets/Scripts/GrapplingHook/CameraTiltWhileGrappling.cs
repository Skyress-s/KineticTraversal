using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTiltWhileGrappling : MonoBehaviour
{
    public InputInfoCenter IIC;

    private Vector3 hook;

    public GameObject Parent;

    public AnimationCurve StartCurve, EndCurve;

    private float startTime;

    private float endTime;


    [Tooltip("How much the camera should tilt when hooked to something")]
    [Range(0f, 1f)]
    public float tiltAmount;

    void Start()
    {
         
    }

    void Update()
    {
        if (IIC.grapplingHookStates.currentState == GrapplingHookStates.GHStates.hooked)
        {


            endTime = 0f;

            hook = IIC.hook.transform.position - transform.position;
            hook = hook.normalized;


            //adds to the time
            startTime += Time.deltaTime;

            startTime = Mathf.Clamp(startTime, 0f, 1f);

            if (hook.y < 0)
            {
                hook = InvertYaxis(hook);
            }

            var v2 = Vector3.Slerp(Vector3.up, hook, StartCurve.Evaluate(startTime) * tiltAmount);
            transform.up = v2;


        }
        else
        {
            transform.localRotation = Quaternion.identity;
            startTime = 0f;

            endTime += Time.deltaTime;

            endTime = Mathf.Clamp(endTime, 0f, 1f);

            var v = Vector3.Slerp(Vector3.up, hook, EndCurve.Evaluate(endTime) * tiltAmount);
            transform.up = v;
        }
    }


    private Vector3 InvertYaxis(Vector3 vec)
    {
        var newvec = new Vector3(vec.x, -vec.y, vec.z);
        return newvec;
    }

}
