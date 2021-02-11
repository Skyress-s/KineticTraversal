using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    private Spring spring;
    private LineRenderer lr;
    public int quality;
    private Vector3 currentGrapplePosition;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHeight;
    public AnimationCurve affectCurve;
    public GrapplingHookStates states;


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        spring = new Spring();
        spring.SetTarget(0);

    }

    private void LateUpdate()
    {
        DrawRope();
    }


    public bool b;
    void DrawRope()
    {
        //if not grappling, dont draw rope
        if (states.currentState.ToString() == "rest")
        {
            currentGrapplePosition = GameObject.Find("Hook").transform.position;
            spring.Reset();
            if (lr.positionCount > 0)
            {
                lr.positionCount = 0;
            }
            return;
        }

        if (lr.positionCount == 0)
        {
            //Debug.Log("posCount = 0");
            spring.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }

        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);

        var grapplePoint = GameObject.Find("Hook").transform.position;
        var gunTipPosition = GameObject.Find("RightHand").transform.position;
        var up = Quaternion.LookRotation(grapplePoint - gunTipPosition).normalized * Vector3.up;

        //currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);
        currentGrapplePosition = grapplePoint + GameObject.Find("Hook").transform.forward * -1.4f;

        for (int i = 0; i < quality + 1; i++)
        {
            var delta = i / ((float)quality);
            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                affectCurve.Evaluate(delta);

            lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
        }
    }
}
