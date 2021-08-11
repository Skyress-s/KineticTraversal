using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private bool _isHit;

    public GHIsCollided Collided;

    public Transform hook;

    private bool hookHasBeenConnected;

    private Rigidbody rb;

    public float originalDist;

    public float pullForce, dampForce;

    private GrapplingHookStates gHstate;

    [SerializeField]
    private float minRopeLentgh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gHstate = hook.gameObject.GetComponent<GrapplingHookStates>();
    }


    void FixedUpdate()
    {
        
        var currentGHState = gHstate.currentState.ToString();
        
        string hooked = "hooked";
        //what to do when the hook is first attached
        if (currentGHState == hooked && hookHasBeenConnected == false)
        {
            //makes it so that the three main grappling hook phases works
            hookHasBeenConnected = true;
            //the rest of the logic
            originalDist = DistanceToHook();
            //Debug.Log(originalDist);
        }


        if (currentGHState == hooked) // what todo if the hook is continously connected to somthing
        {

            //basic pull towards hook if at rope lentgh
            PullTowardsHook(pullForce);


            //dampens the thug so its more smoooooooth
            var var = VelocityTowardsRadius();
            //Debug.Log(var);
            var dir = hook.position - transform.position;
            dir.Normalize();

            if (var > 0)
            {
                var = 0;
            }

            if (DistanceToHook() > originalDist)
            {
                rb.AddForce(dir * -var * dampForce * Time.fixedDeltaTime /** DistanceToHook() * 0.2f*/,
                ForceMode.VelocityChange);
            }
        }


        var dist = DistanceToHook();
        //shortens the original distance so the rope always has tension
        if (originalDist * 0.9f > dist)
        {
            originalDist = DistanceToHook();
        }

        //lentghens the rope if its to short!
        if (minRopeLentgh > dist)
        {
            originalDist = minRopeLentgh;
        }


        //what to do when the hook is disengaged
        if (currentGHState != hooked)
        {
            //makes it so so that the three parts of the 
            hookHasBeenConnected = false;
            //the rest of the logic

        }
    }

    void PullTowardsHook(float force)
    {
        //gets the direction to the hook
        var dir = hook.position - transform.position;
        dir.Normalize();

        //finds the current distance
        float currentDist = 0;
        currentDist = DistanceToHook();

        if (currentDist > originalDist * 1.1f)
        {
            //Debug.Log(currentDist);
            rb.AddForce(dir * force * Time.fixedDeltaTime * (DistanceToHook() - originalDist) /* * DistanceToHook()*0.2f*/, 
                ForceMode.VelocityChange);
        }

    }
    
    float VelocityTowardsRadius()
    {
        // TODO make so that it is the velocity from the radius point, not the hook
        //Finds velocity from player to the point on the radius sphere closest to the player

        Vector3 fromPlayerToHook = hook.transform.position - transform.position;

        //getting from player to point on radius
        var ratio = fromPlayerToHook.magnitude - originalDist;
        //Debug.Log(ratio);
        var playerToRadius = fromPlayerToHook.normalized * ratio;


        var hypotinus = rb.velocity;
        var theta = Vector3.Angle(hypotinus, playerToRadius);
        theta = Mathf.Deg2Rad * theta; // converts from degrees to radians, becouse mathf.cos uses radians
        var speedTowardsRadius = Mathf.Cos(theta) * hypotinus;
        var towardsOrAway = Vector3.Dot(hypotinus.normalized, playerToRadius.normalized);

        if (towardsOrAway > 0)
        {
            towardsOrAway = 1;
        }
        else
        {
            towardsOrAway = -1;
        }


        var velocity = speedTowardsRadius.magnitude * towardsOrAway;

        //Debug.Log(velocity);
        return velocity;
    }

    float DistanceToHook()
    {
        return (Vector3.Distance(transform.position, hook.transform.position));
    }
}
